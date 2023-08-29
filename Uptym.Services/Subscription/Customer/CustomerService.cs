using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Uptym.Core.Common;
using Uptym.Core.Interfaces;
using Uptym.Data.BaseModeling;
using Uptym.Data.DataContext;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.Enums;
using Uptym.DTO.Common;
using Uptym.DTO.Subscription.Customer;
using Uptym.DTO.Subscription.Membership;
using Uptym.Repositories.Configuration.Configuration;
using Uptym.Repositories.Security.UserRole;
using Uptym.Repositories.Subscription.Customer;
using Uptym.Repositories.Subscription.Membership;
using Uptym.Repositories.Subscription.Plan;
using Uptym.Repositories.Subscription.UpcomingPayment;
using Uptym.Repositories.UOW;
using Uptym.Services.Generics;
using Uptym.Services.Global.FileService;
using Uptym.Services.Global.SendEmail;
using Uptym.Services.Global.UploadFiles;

namespace Uptym.Services.Subscription.Customer
{
    public class CustomerService : GService<CustomerDto, Data.DbModels.SubscriptionSchema.Customer, ICustomerRepository>, ICustomerService
    {
        private readonly IResponseDTO _response;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IUpcomingPaymentRepository _upcomingPaymentRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly IEmailService _emailService;
        private readonly IFileService<ExportCustomerDto> _fileService;

        public CustomerService(IMapper mapper,
            IResponseDTO responseDTO,
            IUnitOfWork<AppDbContext> unitOfWork,
            ICustomerRepository customerRepository,
            IMembershipRepository membershipRepository,
            IPlanRepository planRepository,
            IUserRoleRepository userRoleRepository,
            IConfigurationRepository configurationRepository,
            IUpcomingPaymentRepository upcomingPaymentRepository,
            UserManager<ApplicationUser> userManager,
            IUploadFilesService uploadFilesService,
            IEmailService emailService,
            IFileService<ExportCustomerDto> fileService
            ) : base(customerRepository, mapper)
        {
            _response = responseDTO;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _membershipRepository = membershipRepository;
            _planRepository = planRepository;
            _userRoleRepository = userRoleRepository;
            _configurationRepository = configurationRepository;
            _upcomingPaymentRepository = upcomingPaymentRepository;
            _userManager = userManager;
            _uploadFilesService = uploadFilesService;
            _emailService = emailService;
            _fileService = fileService;
        }

        public async Task<IResponseDTO> Register(CustomerDto customerDto, IFormFile file)
        {

            try
            {
                //Create Customer in User table
                var config = _configurationRepository.GetFirst();
                var appUser = _mapper.Map<ApplicationUser>(customerDto);
                appUser.UserName = customerDto.Email;
                appUser.ChangePassword = false;
                appUser.EmailVerifiedDate = null;
                appUser.NextPasswordExpiryDate = DateTime.Now.AddDays(config.NumOfDaysToChangePassword);
                appUser.EmailConfirmed = false;
                appUser.Status = UserStatusEnum.Active.ToString();
             //   appUser.CustomerID = customerDto.Id;

                IdentityResult result = await _userManager.CreateAsync(appUser, customerDto.Password);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Message = $"Code: {result.Errors.FirstOrDefault().Code}, \n Description: {result.Errors.FirstOrDefault().Description}";
                    return _response;
                }
                var path = $"\\Uploads\\Users\\User_{appUser.Id}";
                if (file != null)
                {
                    await _uploadFilesService.UploadFile(path, file, true);
                    appUser.PersonalImagePath = $"\\{path}\\{file.FileName}";
                }

                // Assign roles to the user
                List<ApplicationUserRole> userRoleList = new List<ApplicationUserRole>
                {
                    new ApplicationUserRole
                    {
                        UserId = appUser.Id,
                        RoleId = (int)ApplicationRolesEnum.Admin,
                        Role = null,
                        User = null
                    }
                };
                await _userRoleRepository.AddRangeAsync(userRoleList);


                // Create Customer in Customer Table
                var customer = _mapper.Map<Data.DbModels.SubscriptionSchema.Customer>(customerDto);
                customer.ReminderDays = config.ReminderDays;
                customer.CreatedBy = appUser.Id;
                customer.CustomerType = null;
                customer.Plan = null;
                customer.Location = _mapper.Map<Location>(customerDto.LocationDto);
                await _customerRepository.AddAsync(customer);

                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Customer Not saved";
                    return _response;
                }

                // Create new Trial Membership
                var newMembership = new Data.DbModels.SubscriptionSchema.Membership
                {
                    CustomerId = customer.Id,
                    PlanId = 1, //Trial Plan
                    Status = MembershipStatusEnum.Active.ToString(),
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(config.TrialPeriodDays),
                    ExtraEndDate = DateTime.Now.AddDays(config.TrialPeriodDays),
                    AutoActive = false,
                    CreatedBy = appUser.Id,
                    CreatedOn = DateTime.Now,
                    Customer = null,
                    Plan = null
                };
                await _membershipRepository.AddAsync(newMembership);
                appUser.CustomerID = customer.Id;// Assigning Customer ID For Admin user when being registered

                save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Membership Not saved";
                    return _response;
                }
                // Token to verify the email
                var verifyEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                verifyEmailToken = WebUtility.UrlEncode(verifyEmailToken);
                // send email
                await _emailService.SendEmailConfirmationRequest(customerDto.Email, verifyEmailToken);

                _response.Data = new
                {
                    CustomerId = customer.Id
                    
                };
                _response.IsPassed = true;
                _response.Message = "Ok";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Error " + ex.Message;
            }
            return _response;
        }

        public IResponseDTO GetAllAsDrp(CustomerFilterDto filterDto = null)
        {
            try
            {
                var query = _customerRepository.GetAll(x => !x.IsDeleted);

                if (filterDto != null)
                {
                    if (filterDto.IsActive != null)
                    {
                        query = query.Where(x => x.IsActive == filterDto.IsActive);
                    }
                    if (filterDto.CustomerTypeId > 0)
                    {
                        query = query.Where(x => x.CustomerTypeId == filterDto.CustomerTypeId);
                    }
                    if (filterDto.Id > 0)
                    {
                        query = query.Where(x => x.Id == filterDto.Id);
                    }
                }
                
                //query = query.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
                var dataList = _mapper.Map<List<CustomerDrp>>(query.ToList());

                int i = 0;
                foreach (var item in query)
                {

                    if (item.Organization != null)
                    {
                        dataList[i].Name = item.Organization;
                     }
                    else
                    {
                        dataList[i].Name = item.Email;
                    }

                    i++;
                }

                _response.Data = dataList;
                _response.IsPassed = true;
                _response.Message = "Done";
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Error " + ex.Message;
            }
            return _response;
        }

        public async Task<IResponseDTO> GetCustomerDetails(int customerId)
        {
            var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Id == customerId);
            if (customer == null)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Email is not found";
                return _response;
            }
            var customerDetailsDto = _mapper.Map<CustomerDto>(customer);
            _response.IsPassed = true;
            _response.Data = customerDetailsDto;
            return _response;
        }
        public async Task<IResponseDTO> GetCustomerDetailsByUserId(int loggedInUserId)
        {
           
            var appUser = await _userManager.FindByIdAsync(loggedInUserId.ToString());
      

            if (appUser == null)
            {
                _response.IsPassed = false;
                _response.Message = "User not found";
                return _response;
            }

          // var customer = await _customerRepository.GetAll().Where(x => x.Id == appUser.CustomerID).FirstOrDefaultAsync().PlanId;
          //var customer = await _customerRepository.GetAll().Include(x => x.Plan).FirstOrDefaultAsync(x => x.Email == appUser.Email);
             var customer = await _customerRepository.GetAll().Include(x => x.Plan).FirstOrDefaultAsync(x => x.Id == appUser.CustomerID);
            if (customer == null)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Customer Email is not found";
                return _response;
            }
            var customerDetailsDto = _mapper.Map<CustomerDto>(customer);
            _response.IsPassed = true;
            _response.Data = customerDetailsDto;
            return _response;
        }
        public async Task<IResponseDTO> GetCustomerMembershipInfo(int loggedInUserId)
        {
            var appUser = await _userManager.FindByIdAsync(loggedInUserId.ToString());
            if (appUser == null)
            {
                _response.IsPassed = false;
                _response.Message = "User not found";
                return _response;
            }
            var customer = _customerRepository.GetFirstOrDefault(x => x.Email == appUser.Email);
            if (customer == null)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Email is not found";
                return _response;
            }
            var membership = await _membershipRepository.GetAll().Include(x => x.Plan)
                .FirstOrDefaultAsync(x => x.CustomerId == customer.Id && x.PlanId == customer.PlanId);
            if (membership == null)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Membership is not found";
                return _response;
            }
            var membershipDto = _mapper.Map<MembershipDto>(membership);
            membershipDto.RemainDays = (int)(membership.EndDate.Date - DateTime.Today.Date).TotalDays;
            _response.Data = membershipDto;
            _response.IsPassed = true;
            _response.Message = "Ok";
            return _response;
        }
        public IResponseDTO GetAll(int? pageIndex = null, int? pageSize = null, CustomerFilterDto filterDto = null)
        {
            try
            {
                // get users with roles
                IQueryable<Data.DbModels.SubscriptionSchema.Customer> customers = _customerRepository.GetAll();

                if (filterDto != null)
                {

                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        customers = customers.Where(u => u.FirstName.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()) || u.LastName.Contains(filterDto.Name.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Email))
                    {
                        customers = customers.Where(u => u.Email.Trim().ToLower().Contains(filterDto.Email.Trim().ToLower()));
                    }
                    if (filterDto.PlanId != 0)
                    {
                        customers = customers.Where(x => x.PlanId == filterDto.PlanId);
                    }
                    if (filterDto.CustomerTypeId != 0)
                    {
                        customers = customers.Where(x => x.CustomerTypeId == filterDto.CustomerTypeId);
                    }
                    if (!string.IsNullOrEmpty(filterDto.Status))
                    {
                        customers = customers.Where(u => u.Status.Trim().ToLower().Equals(filterDto.Status.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.IsTrial))
                    {
                        customers = customers.Where(u => u.IsTrial == (filterDto.IsTrial == "Yes"));
                    }
                    if (!string.IsNullOrEmpty(filterDto.PhoneNumber))
                    {
                        customers = customers.Where(u => u.PhoneNumber.Trim().ToLower().Contains(filterDto.PhoneNumber.Trim().ToLower()));
                    }

                    if (filterDto == null || (filterDto != null && string.IsNullOrEmpty(filterDto.SortProperty)))
                    {
                        customers = customers.OrderByDescending(x => x.Id);
                    }
                }

                var total = customers.Count();

                // Apply Pagination
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    customers = customers.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var customersList = _mapper.Map<List<CustomerDto>>(customers.ToList());

                _response.Data = new
                {
                    List = customersList,
                    Page = pageIndex ?? 0,
                    pageSize = pageSize ?? 0,
                    Total = total,
                    Pages = pageSize.HasValue && pageSize.Value > 0 ? total / pageSize : 1
                };


                _response.Message = "Ok";
                _response.IsPassed = true;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }
        public async Task<IResponseDTO> CreateCustomer(CustomerDto customerDto, IFormFile file)
        {
            try
            {
                var config = _configurationRepository.GetFirst();

                // Generate user password
                customerDto.Password = GeneratePassword();

                var appUser = _mapper.Map<ApplicationUser>(customerDto);
                appUser.UserName = customerDto.Email;
                appUser.ChangePassword = true;
                appUser.EmailVerifiedDate = null;
                appUser.NextPasswordExpiryDate = DateTime.Now.AddDays(config.NumOfDaysToChangePassword);

                IdentityResult result = await _userManager.CreateAsync(appUser, customerDto.Password);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Message = $"Code: {result.Errors.FirstOrDefault().Code}, \n Description: {result.Errors.FirstOrDefault().Description}";
                    return _response;
                }

                var path = $"\\Uploads\\Users\\User_{appUser.Id}";
                if (file != null)
                {
                    await _uploadFilesService.UploadFile(path, file, true);
                    appUser.PersonalImagePath = $"\\{path}\\{file.FileName}";
                }

                // Assign roles to the user
                List<ApplicationUserRole> userRoleList = new List<ApplicationUserRole>
                {
                    new ApplicationUserRole
                    {
                        UserId = appUser.Id,
                        RoleId = (int)ApplicationRolesEnum.Admin,
                        Role = null,
                        User = null
                    }
                };
                await _userRoleRepository.AddRangeAsync(userRoleList);


                // Create Customer in Customer Table
                var customer = _mapper.Map<Data.DbModels.SubscriptionSchema.Customer>(customerDto);
                customer.CreatedBy = appUser.Id;
                customer.CustomerType = null;
                customer.Plan = null;
                customer.Location = _mapper.Map<Location>(customerDto.LocationDto);
                await _customerRepository.AddAsync(customer);

                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to register the customer";
                    return _response;
                }

                //// Token to reset tha pass
                //var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                //resetPassToken = WebUtility.UrlEncode(resetPassToken);
                //// send email
                //await _emailService.AfterRegistiration(CustomerDto.Email, resetPassToken);

                _response.IsPassed = true;
                _response.Message = "Customer created Successfully";
                _response.Data = _mapper.Map<CustomerDto>(customer);
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        public async Task<IResponseDTO> UpdateCustomer(string rootPath, CustomerDto customerDto, IFormFile file)
        {
            try
            {
                // Update Customer in Customer Table
                var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Id == customerDto.Id);

                var appUser = await _userManager.FindByEmailAsync(customer.Email);
                var path = $"\\Uploads\\Users\\User_{appUser.Id}";
                if (file != null && !customerDto.ReomveProfileImage)
                {
                    await _uploadFilesService.UploadFile(path, file, true);
                    appUser.PersonalImagePath = $"\\{path}\\{file.FileName}";
                }
                else if (customerDto.ReomveProfileImage)
                {
                    appUser.PersonalImagePath = null;
                }


                // Update the user props
                appUser.FirstName = customerDto.FirstName;
                appUser.LastName = customerDto.LastName;
                appUser.Email = customerDto.Email;
                appUser.PhoneNumber = customerDto.PhoneNumber;
                appUser.Address = customerDto.Address;

                var result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Message = $"Code: {result.Errors.FirstOrDefault().Code}, \n Description: {result.Errors.FirstOrDefault().Description}";
                    return _response;
                }

                customer.UpdatedBy = appUser.Id;
                customer.UpdatedOn = customerDto.UpdatedOn;
                customer.FirstName = customerDto.FirstName;
                customer.LastName = customerDto.LastName;
                customer.Email = customerDto.Email;
                customer.PhoneNumber = customerDto.PhoneNumber;
                customer.CustomerTypeId = customerDto.CustomerTypeId;
                customer.Organization = customerDto.Organization;
                customer.Address = customerDto.Address;
                customer.ReminderDays = customerDto.ReminderDays;
                customer.CustomerType = null;
                customer.Plan = null;
                customer.Location = _mapper.Map<Location>(customerDto.LocationDto);
                _customerRepository.Update(customer);

                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to update the customer";
                    return _response;
                }

                // Res
                var customerResult = _mapper.Map<CustomerDto>(customerDto);
                if (!string.IsNullOrEmpty(customerResult.PersonalImagePath))
                {
                    customerResult.PersonalImagePath = rootPath + customerResult.PersonalImagePath;
                }

                _response.IsPassed = true;
                _response.Message = "Profile is updated successfully";
                _response.Data = customerResult;

            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        public async Task<IResponseDTO> UpdateCustomerReminder(int loggedInUserId, int reminderDays)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(loggedInUserId.ToString());
                if (appUser == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "User not found";
                    return _response;
                }

                var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Email == appUser.Email);
                if (customer == null)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Email is not found";
                    return _response;
                }
                customer.ReminderDays = reminderDays;
                _customerRepository.Update(customer);

                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to update the customer";
                    return _response;
                }
                _response.IsPassed = true;
                _response.Message = "Updated successfully";
                _response.Data = null;

            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;

        }
        public async Task<IResponseDTO> UpdateCustomerStatus(int customerId, string status, LocationDto locationDto)
        {
            try
            {
                var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Id == customerId);
                var appUser = await _userManager.FindByEmailAsync(customer.Email);
                if (customer == null)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Customer not found";
                    return _response;
                }
                if (customer.Status == status)
                {
                    _response.IsPassed = false;
                    _response.Message = $"Customer is already {status}";
                    return _response;
                }

                appUser.Status = status;
                if (status == UserStatusEnum.Active.ToString())
                {
                    appUser.AccessFailedCount = 0;
                }
                customer.Status = status;

                // Update the user in database
                await _userManager.UpdateAsync(appUser);
                _customerRepository.Update(customer);

                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Not updated";
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Done";
                _response.Data = null;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        // Helper Methods
        private string GeneratePassword()
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            var result = Regex.Replace(password.ToString(), @"[^0-9a-zA-Z]+", "$");
            result += RandomString(6);

            return result;
        }
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdrfghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public async Task<GeneratedFile> ExportUsers(int? pageIndex = null, int? pageSize = null, CustomerFilterDto filterDto = null)
        {
            try
            {
                // get users with roles
                IQueryable<Data.DbModels.SubscriptionSchema.Customer> customers = await _customerRepository.GetAllAsync();

                if (filterDto != null)
                {

                    if (filterDto.PlanId != 0)
                    {
                        customers = customers.Where(x => x.PlanId == filterDto.PlanId);
                    }
                    if (!string.IsNullOrEmpty(filterDto.Email))
                    {
                        customers = customers.Where(u => u.Email.Trim().ToLower().Contains(filterDto.Email.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.PhoneNumber))
                    {
                        customers = customers.Where(u => u.PhoneNumber.Trim().ToLower().Contains(filterDto.PhoneNumber.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Status))
                    {
                        customers = customers.Where(u => u.Status.Trim().ToLower().Contains(filterDto.Status.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        customers = customers.Where(u => u.FirstName.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()) || u.LastName.Contains(filterDto.Name.Trim().ToLower()));
                    }

                    if (filterDto == null || (filterDto != null && string.IsNullOrEmpty(filterDto.SortProperty)))
                    {
                        customers = customers.OrderByDescending(x => x.Id);
                    }
                }


                // Apply Pagination
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    customers = customers.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var dataList = _mapper.Map<List<ExportCustomerDto>>(customers.ToList());

                return _fileService.ExportToExcel(dataList);
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return null;
        }
    }
}
