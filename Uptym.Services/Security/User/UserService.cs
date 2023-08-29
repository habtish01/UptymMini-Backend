using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Uptym.Core.Common;
using Uptym.Core.Interfaces;
using Uptym.Data.DataContext;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.Enums;
using Uptym.DTO.Common;
using Uptym.DTO.Security;
using Uptym.DTO.Security.User;
using Uptym.Repositories.Configuration.Configuration;
using Uptym.Repositories.Maintenance;
using Uptym.Repositories.Metadata;
using Uptym.Repositories.Security.UserRole;
using Uptym.Repositories.Subscription.Customer;
using Uptym.Repositories.UOW;
using Uptym.Services.Global.FileService;
using Uptym.Services.Global.SendEmail;
using Uptym.Services.Global.UploadFiles;
using Uptym.Services.Metadata;

namespace Uptym.Services.Security.User
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IAssignedEngineerRepository _assignedEngineerRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IHealthFacilityRepository _healthfacilityRepository;
        private readonly IHealthFacilityTypeRepository _healthfacilitytypeRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IResponseDTO _response;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly IEmailService _emailService;
        private readonly IFileService<ExportUserDto> _fileService;


        public UserService(
            IUnitOfWork<AppDbContext> unitOfWork,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IHealthFacilityRepository  healthFacilityRepository,
            IHealthFacilityTypeRepository healthFacilityTypeRepository,
            IResponseDTO responseDTO,
            IUserRoleRepository userRoleRepository,
            IAssignedEngineerRepository assignedEngineerRepository,
            RoleManager<ApplicationRole> roleManager,
            IUploadFilesService uploadFilesService,
            IEmailService emailService,
            IConfigurationRepository configurationRepository,
            ICustomerRepository customerRepository,
            IFileService<ExportUserDto> fileService
            )
        {
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _healthfacilityRepository = healthFacilityRepository;
            _healthfacilitytypeRepository = healthFacilityTypeRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _assignedEngineerRepository = assignedEngineerRepository;
            _response = responseDTO;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _emailService = emailService;
            _configurationRepository = configurationRepository;
            _customerRepository = customerRepository;
            _fileService = fileService;
        }

        public async Task<IResponseDTO> GetAllUsers(string loggedInUserName, bool isAdmin, string rootPath, int? pageIndex = null, int? pageSize = null, UserFilterDto filterDto = null)
        {
            try
            {
                // get users with roles
                IQueryable<ApplicationUser> appUsers = _userManager.Users;
                if (isAdmin)
                {
                    var customer = _customerRepository.GetFirstOrDefault(x => x.Email == loggedInUserName);
                    appUsers = appUsers.Where(x => x.CustomerID == customer.Id && x.CreatedBy !=null);// If Created by is NULL then the user is Admin
                }

                if (filterDto != null)
                {

                    if (filterDto.RoleId != 0)
                    {
                        var role = _roleManager.Roles.FirstOrDefault(r => r.Id == filterDto.RoleId);
                        if (role != null)
                        {
                            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                            var usersInRoleIds = usersInRole.Select(x => x.Id);
                            appUsers = appUsers.Where(u => usersInRoleIds.Contains(u.Id));
                        }
                    }
                    if (!string.IsNullOrEmpty(filterDto.Email))
                    {
                        appUsers = appUsers.Where(u => u.Email.Trim().ToLower().Contains(filterDto.Email.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.PhoneNumber))
                    {
                        appUsers = appUsers.Where(u => u.PhoneNumber.Trim().ToLower().Contains(filterDto.PhoneNumber.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Status))
                    {
                        appUsers = appUsers.Where(u => u.Status.Trim().ToLower().Contains(filterDto.Status.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        appUsers = appUsers.Where(u => u.FirstName.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()) || u.LastName.Contains(filterDto.Name.Trim().ToLower()));
                    }


                    if (filterDto == null || (filterDto != null && string.IsNullOrEmpty(filterDto.SortProperty)))
                    {
                        appUsers = appUsers.OrderByDescending(x => x.Id);
                    }
                }

                var total = appUsers.Count();


                //Check Sort Property
                //if (filterDto != null && !string.IsNullOrEmpty(filterDto.SortProperty))
                //{
                //    appUsers = appUsers.AsQueryable().OrderBy(
                //     string.Format("{0} {1}", filterDto.SortProperty, filterDto.IsAscending ? "ASC" : "DESC"));
                //}
                // Apply Pagination
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    appUsers = appUsers.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var usersList = _mapper.Map<List<UserDto>>(appUsers.ToList());

                foreach (var user in usersList)
                {
                    user.UserRoles = GetRoles(user.Id);

                    if (!string.IsNullOrEmpty(user.PersonalImagePath))
                    {
                        user.PersonalImagePath = rootPath + user.PersonalImagePath;
                    }

                }

              
                foreach(var sites in usersList){

                    var hFacility = await _healthfacilityRepository.GetAll()
                                     .FirstOrDefaultAsync(x => x.Id == sites.HealthFacilityId);
                    var hTypeFacility = await _healthfacilitytypeRepository.GetAll()
                                    .FirstOrDefaultAsync(x => x.Id == sites.HealthFacilityTypeId);
                    if (hFacility != null)
                    {
                        sites.HealthFacilityName = hFacility.Name;
                    }
                    if (hTypeFacility != null)
                    {
                        sites.HealthFacilityTypeName = hTypeFacility.Name;
                    }

                }


                _response.Data = new
                {
                    List = usersList,
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

        public async Task<IResponseDTO> GetSingleSite(int healthFacilityId)
        {
            try
            {
                var hFacility = await _healthfacilityRepository.GetAll()
                                        .Include(x => x.Name)
                                        .FirstOrDefaultAsync(x => x.Id == healthFacilityId);

            
                if (hFacility == null)
                {
                    _response.Message = "Invalid object id";
                    _response.IsPassed = false;
                    return _response;
                }

                var userDTO = _mapper.Map<UserDto>(hFacility);

                _response.Data = userDTO;
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
        public async Task<IResponseDTO> GetAllOpianUsers(string rootPath, int? pageIndex = null, int? pageSize = null, UserFilterDto filterDto = null)
        {
            try
            {
                // get users with roles
                IQueryable<ApplicationUser> appUsers = _userManager.Users;
                var opianRoles = _roleManager.Roles.Where(x => x.RoleType == 1).Select(x => x.Id).ToList();
                var opianUserIds = _userRoleRepository.GetAll(x => opianRoles.Contains(x.RoleId)).Select(x => x.UserId).ToList();
                appUsers = appUsers.Where(u => opianUserIds.Contains(u.Id));
                if (filterDto != null)
                {

                    if (filterDto.RoleId != 0)
                    {
                        var role = _roleManager.Roles.FirstOrDefault(r => r.Id == filterDto.RoleId);
                        if (role != null)
                        {
                            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                            var usersInRoleIds = usersInRole.Select(x => x.Id);
                            appUsers = appUsers.Where(u => usersInRoleIds.Contains(u.Id));
                        }
                    }
                    if (!string.IsNullOrEmpty(filterDto.Email))
                    {
                        appUsers = appUsers.Where(u => u.Email.Trim().ToLower().Contains(filterDto.Email.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.PhoneNumber))
                    {
                        appUsers = appUsers.Where(u => u.PhoneNumber.Trim().ToLower().Contains(filterDto.PhoneNumber.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Status))
                    {
                        appUsers = appUsers.Where(u => u.Status.Trim().ToLower().Contains(filterDto.Status.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        appUsers = appUsers.Where(u => u.FirstName.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()) || u.LastName.Contains(filterDto.Name.Trim().ToLower()));
                    }


                    if (filterDto == null || (filterDto != null && string.IsNullOrEmpty(filterDto.SortProperty)))
                    {
                        appUsers = appUsers.OrderByDescending(x => x.Id);
                    }
                }

                var total = appUsers.Count();

                // Apply Pagination
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    appUsers = appUsers.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var usersList = _mapper.Map<List<UserDto>>(appUsers.ToList());

                foreach (var user in usersList)
                {
                    user.UserRoles = GetRoles(user.Id);

                    if (!string.IsNullOrEmpty(user.PersonalImagePath))
                    {
                        user.PersonalImagePath = rootPath + user.PersonalImagePath;
                    }

                }

                _response.Data = new
                {
                    List = usersList,
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
        public async Task<IResponseDTO> GetAllAsDrp(UserFilterDto filterDto = null)
        {
            try
            {
                IQueryable<ApplicationUser> appUsers = _userManager.Users;
              

                if (filterDto != null)
                {

                    if (filterDto.RoleId != 0)
                    {
                        var role = _roleManager.Roles.FirstOrDefault(r => r.Id == filterDto.RoleId);
                        if (role != null)
                        {
                            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                            var usersInRoleIds = usersInRole.Select(x => x.Id);
                            appUsers = appUsers.Where(u => usersInRoleIds.Contains(u.Id));
                        }
                    }
                    if (filterDto.CustomerId.HasValue && filterDto.CustomerId > 0)
                    {
                        appUsers = appUsers.Where(u => u.CustomerID == filterDto.CustomerId);
                    }
                    if (filterDto.HealthFacilityId.HasValue && filterDto.HealthFacilityId > 0)
                    {
                        appUsers = appUsers.Where(u => u.HealthFacilityId == filterDto.HealthFacilityId);
                    }
                    if (filterDto.CustomerTypeId.HasValue && filterDto.CustomerTypeId > 0)
                    {
                        appUsers = appUsers.Where(u => u.Customer.CustomerTypeId == filterDto.CustomerTypeId);
                    }

                    foreach (var sites in appUsers)
                    {
                        if (sites.HealthFacilityId != null)
                        {
                            var siteName = _healthfacilityRepository.GetAll().Where(v => v.Id == sites.HealthFacilityId).FirstOrDefault().Name;
                            sites.PersonalImagePath = siteName;
                        }
                       


                    }

                }
             

                appUsers = appUsers.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
                var dataList = _mapper.Map<List<UserDrp>>(appUsers.ToList());

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
        public List<string> GetSite(int HealthFacilityId)
        {
            List<string> result = new List<string>();
            try
            {
                var roleNames = _healthfacilityRepository.GetAll(x => x.Id == HealthFacilityId).Select(x => x.Name);

               
                result = roleNames.ToList();
            }
            catch (Exception ex)
            {
                var message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
            }

            return result;
        }
        public List<string> GetRoles(int userId)
        {
            List<string> result = new List<string>();
            try
            {
                var roleIds = _userRoleRepository.GetAll(x => x.UserId == userId).Select(x => x.RoleId);

                var roleNames = _roleManager.Roles.Where(x => roleIds.Contains(x.Id)).Select(x => x.Name);
                result = roleNames.ToList();
            }
            catch (Exception ex)
            {
                var message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
            }

            return result;
        }
        public async Task<IResponseDTO> GetUserDetails(string rootPath, int userId)
        {
            var appUser = await _userManager.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == userId);
            var userDetailsDto = _mapper.Map<UserDto>(appUser);
            userDetailsDto.UserRoleLevels = new List<int>();

            for (int i = 0; i < appUser.UserRoles.Count; i++)
            {
                var roleId = appUser.UserRoles.ElementAt(i).RoleId;
                userDetailsDto.UserRoleLevels.Add(roleId);

            }
            if (!string.IsNullOrEmpty(userDetailsDto.PersonalImagePath))
            {
                userDetailsDto.PersonalImagePath = rootPath + userDetailsDto.PersonalImagePath;
            }

            _response.IsPassed = true;
            _response.Data = userDetailsDto;
            return _response;
        }
        public async Task<IResponseDTO> GetUserDetails2(string rootPath, int userId)
        {
            var appUser = await _userManager.FindByIdAsync("" + userId);
            var id = appUser.CustomerID;
            var userDetailsDto = _mapper.Map<UserDto>(appUser);
            userDetailsDto.UserRoleLevels = new List<int>();

            for (int i = 0; i < appUser.UserRoles.Count; i++)
            {
                var roleId = appUser.UserRoles.ElementAt(i).RoleId;
                userDetailsDto.UserRoleLevels.Add(roleId);

            }
            if (!string.IsNullOrEmpty(userDetailsDto.PersonalImagePath))
            {
                userDetailsDto.PersonalImagePath = rootPath + userDetailsDto.PersonalImagePath;
            }
            if (userDetailsDto.CustomerId == null)
            {
                var customer = _customerRepository.GetFirstOrDefault(x => x.Email == appUser.Email);
                if (customer != null)
                    userDetailsDto.CustomerId = customer.Id;
            }
            _response.IsPassed = true;
            _response.Data = userDetailsDto;
            return _response;
        }
        public async Task<IResponseDTO> GetUserByEmailAsync(string email)
        {
            var appUser = new ApplicationUser();
            try
            {
                appUser = await _userManager.FindByEmailAsync(email);

                _response.IsPassed = true;
                _response.Data = appUser;
            }
            catch (Exception Ex)
            {
                _response.IsPassed = false;
                _response.Data = null;
            }

            return _response;
        }
        public async Task<IResponseDTO> GetUserEmails(int? userId,  int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum)
        {
            IQueryable<ApplicationUser> appUsers = _userManager.Users;



            var appUser = new List<ApplicationUser>();
            List<string> EmailList = new List<string>();

            // Filtering

            if (userId > 0 && userId != null)
            {
                appUser.Add(await _userManager.FindByIdAsync("" + userId));
            }
            if (facilityId > 0 && facilityId != null)
            {
                appUser = appUsers.Where(x => x.HealthFacilityId == facilityId && x.UserRoles.Any(r => r.RoleId == (int)rolesEnum)).ToList();
            }
            if (customerId > 0 && customerId != null)
            {
                appUser = appUsers.Where(x => x.CustomerID == customerId && x.UserRoles.Any(r => r.RoleId == (int)rolesEnum)).ToList();
            }

            // adding emails to the list
            foreach (var value in appUser)
            {
                EmailList.Add(value.Email);
            }

            _response.IsPassed = true;
            _response.Data = EmailList;

            return _response;
        }
        public async Task<IResponseDTO> GetUsersEmails(int? userId, int? WorkorderID)
        {
            IQueryable<ApplicationUser> appUsers = _userManager.Users;



            var appUser = new List<ApplicationUser>();
            List<string> EmailList = new List<string>();



         
            // adding emails to the list
            EmailList = assignedEngineer((int)WorkorderID);

            _response.IsPassed = true;
            _response.Data = EmailList;

            return _response;
        }
        public async Task<IResponseDTO> GetUserPhoneNumbers(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum)
        {
            IQueryable<ApplicationUser> appUsers = _userManager.Users;



            var appUser = new List<ApplicationUser>();
            List<string> EmailList = new List<string>();


            // Filtering

            if (userId > 0 && userId != null)
            {
                appUser.Add(await _userManager.FindByIdAsync("" + userId));
            }
            if (facilityId > 0 && facilityId != null)
            {
                appUser = appUsers.Where(x => x.HealthFacilityId == facilityId && x.UserRoles.Any(r => r.RoleId == (int)rolesEnum)).ToList();
            }
            if (customerId > 0 && customerId != null)
            {
                appUser = appUsers.Where(x => x.CustomerID == customerId && x.UserRoles.Any(r => r.RoleId == (int)rolesEnum)).ToList();
            }

            // adding emails to the list
            foreach (var value in appUser)
            {
                if (value.PhoneNumber != null) EmailList.Add(value.PhoneNumber);
            }

            _response.IsPassed = true;
            _response.Data = EmailList;

            return _response;
        }
        public async Task<IResponseDTO> GetManagerName(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum)
        {
            IQueryable<ApplicationUser> appUsers = _userManager.Users;



            var appUser = new List<ApplicationUser>();
            string  NameList = null;


            // Filtering
            if(userId>0)
            {
                appUser = appUsers.Where(x => x.Id == userId ).ToList();
            }

            // adding emails to the list
            foreach (var value in appUser)
            {
                NameList=value.FirstName + " " + value.LastName;
            }

            _response.IsPassed = true;
            _response.Data = NameList;

            return _response;
        }
        public async Task<IResponseDTO> GetFacilityNames(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum)
        {
            IQueryable<ApplicationUser> appUsers = _userManager.Users;



            var appUser = new List<ApplicationUser>();
            string  NameList = null;


            // Filtering
            if(userId>0)
            {
                appUser = appUsers.Where(x => x.Id == userId ).ToList();
            }

            // adding emails to the list
            foreach (var value in appUser)
            {
                NameList=value.FirstName + " " + value.LastName;
            }

            _response.IsPassed = true;
            _response.Data = NameList;

            return _response;
        }
        public async Task<IResponseDTO> GetManagerNames(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum)
        {
            IQueryable<ApplicationUser> appUsers = _userManager.Users;



            var appUser = new List<ApplicationUser>();
            string  NameList = null;


            // Filtering
            if(userId>0)
            {
                appUser = appUsers.Where(x => x.Id == userId ).ToList();
            }

            // adding emails to the list
            foreach (var value in appUser)
            {
                NameList=value.FirstName + " " + value.LastName;
            }

            _response.IsPassed = true;
            _response.Data = NameList;

            return _response;
        }
        public async Task<IResponseDTO> GetEngineerNames(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum)
        {
            IQueryable<ApplicationUser> appUsers = _userManager.Users;



            var appUser = new List<ApplicationUser>();
            string NameList = null;


            // Filtering
            if (userId > 0)
            {
                appUser = appUsers.Where(x => x.Id == userId).ToList();
            }

            // adding emails to the list
            foreach (var value in appUser)
            {
                NameList = value.FirstName + " " + value.LastName;
            }

            _response.IsPassed = true;
            _response.Data = NameList;

            return _response;
        }
        public List<string> assignedEngineer(int worKOrderId)
        {


            
            IQueryable<ApplicationUser> appUsers = _userManager.Users;
            List<string> result = new List<string>();
            try
            {
                var aes = _assignedEngineerRepository.GetAll(x => x.WorkOrderID == worKOrderId).Select(x => x.AssignedEngineersId);
                var aeEmail = appUsers.Where(x => aes.Contains(x.Id)).Select(x => x.Email);
                result = aeEmail.ToList();
            }
            catch (Exception ex)
            {
                var message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
            }

            return result;
        }
        public async Task<IResponseDTO> CreateUser(int loggedInUserId, bool isAdmin, UserDto userDto, IFormFile file)
        {
            try
            {
                var config = _configurationRepository.GetFirst();

                // Generate user password
                userDto.Password = GeneratePassword();
                

                var appUser = _mapper.Map<ApplicationUser>(userDto);
                var customerUser = new ApplicationUser();
                if (isAdmin)
                {
                    customerUser = await _userManager.FindByIdAsync(loggedInUserId.ToString());
                    var customer = _customerRepository.GetFirstOrDefault(x => x.Email == customerUser.Email);
                    appUser.CustomerID = customer.Id;
                }

                appUser.UserName = userDto.Email;
                appUser.ChangePassword = true;
                appUser.EmailVerifiedDate = null;
                appUser.NextPasswordExpiryDate = DateTime.Now.AddDays(config.NumOfDaysToChangePassword);

                IdentityResult result = await _userManager.CreateAsync(appUser, userDto.Password);
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
                var roleLevels = userDto.UserRoleLevels.ToList();
                List<ApplicationUserRole> userRoleList = new List<ApplicationUserRole>();
                for (int i = 0; i < roleLevels.Count; i++)
                {
                    userRoleList.Add(new ApplicationUserRole
                    {
                        UserId = appUser.Id,
                        RoleId = roleLevels[i]
                    });
                }

                await _userRoleRepository.AddRangeAsync(userRoleList);

                // Commit to database
                var finalResult = await _unitOfWork.CommitAsync();
                if (finalResult == 0)
                {
                    _response.IsPassed = false;
                    _response.Message = "Faild to create the user";
                    return _response;
                }

                // Token to reset tha pass
                var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                resetPassToken = WebUtility.UrlEncode(resetPassToken);
                // send email
                if (isAdmin)
                {
                    await _emailService.CreateUserForUser(appUser.Email, customerUser.UserName, appUser.UserName, userDto.UserRoleLevelName, resetPassToken);
                    await _emailService.CreateUserForAdmin(customerUser.Email, customerUser.UserName, appUser.UserName, userDto.UserRoleLevelName);
                }
                else
                {
                    await _emailService.CreateUserForUser(appUser.Email, "SuperAdmin", appUser.UserName, userDto.UserRoleLevelName, resetPassToken);
                }

                _response.IsPassed = true;
                _response.Message = "User is created successfully";
                _response.Data = _mapper.Map<UserDto>(appUser);

            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }
        public async Task<IResponseDTO> Register(UserDto userDto, IFormFile file)
        {
            try
            {
                var config = _configurationRepository.GetFirst();

                var appUser = _mapper.Map<ApplicationUser>(userDto);
                appUser.UserName = userDto.Email;
                appUser.ChangePassword = false;
                appUser.EmailVerifiedDate = null;
                appUser.NextPasswordExpiryDate = DateTime.Now.AddDays(config.NumOfDaysToChangePassword);
                // Reset

                IdentityResult result = await _userManager.CreateAsync(appUser, userDto.Password);
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


                // Commit to database
                var finalResult = await _unitOfWork.CommitAsync();
                if (finalResult == 0)
                {
                    _response.IsPassed = false;
                    _response.Message = "Faild to register the user";
                    return _response;
                }

                // Token to verify the email
                var verifyEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                verifyEmailToken = WebUtility.UrlEncode(verifyEmailToken);

                // send email
                await _emailService.SendEmailConfirmationRequest(userDto.Email, verifyEmailToken);

                _response.IsPassed = true;
                _response.Message = "You are registred successfully";
                _response.Data = _mapper.Map<UserDto>(appUser);
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }
        public async Task<IResponseDTO> UpdateUser(string rootPath, UserDto userDto, IFormFile file)
        {
            // When Updating Profile => AccountController.UpdateUserProfile
            try
            {

                var appUser = await _userManager.FindByIdAsync(userDto.Id.ToString());
                var path = $"\\Uploads\\Users\\User_{appUser.Id}";
                if (file != null && !userDto.ReomveProfileImage)
                {
                    await _uploadFilesService.UploadFile(path, file, true);
                    appUser.PersonalImagePath = $"\\{path}\\{file.FileName}";
                }
                else if (userDto.ReomveProfileImage)
                {
                    appUser.PersonalImagePath = null;
                }

                // Old Roles id 
                var oldRoles = _userRoleRepository.GetAll(x => x.UserId == userDto.Id);
                _userRoleRepository.RemoveRange(oldRoles);

                // Update the user props
                appUser.FirstName = userDto.FirstName;
                appUser.LastName = userDto.LastName;
                if (userDto.Email != appUser.Email)
                {

                    var verifyEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    verifyEmailToken = WebUtility.UrlEncode(verifyEmailToken);

                    // send email
                    await _emailService.SendEmailConfirmationRequest(userDto.Email, verifyEmailToken);
                }
                appUser.Email = userDto.Email;
                appUser.PhoneNumber = userDto.PhoneNumber;
                appUser.Address = userDto.Address;
                appUser.Education = userDto.Education;
                appUser.Specialties = userDto.Specialties;
                appUser.Experience = userDto.Experience;
                appUser.Position = userDto.Position;
                appUser.HealthFacilityId = userDto.HealthFacilityId;

               
                var result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Message = $"Code: {result.Errors.FirstOrDefault().Code}, \n Description: {result.Errors.FirstOrDefault().Description}";
                    return _response;
                }

                // Check User Subscription
                var roleLevels = userDto.UserRoleLevels.ToList();
                List<ApplicationUserRole> userRoleList = new List<ApplicationUserRole>();
                for (int i = 0; i < roleLevels.Count; i++)
                {
                    userRoleList.Add(new ApplicationUserRole
                    {
                        UserId = appUser.Id,
                        RoleId = roleLevels[i]
                    });
                }
                await _userRoleRepository.AddRangeAsync(userRoleList);

                // Check Customer's Organization
                var customer = _customerRepository.GetFirstOrDefault(x => x.Email == userDto.Email);
                if (customer != null && userDto.Organization != null)
                {
                    customer.FirstName = userDto.FirstName;
                    customer.LastName = userDto.LastName;
                    customer.Address = userDto.Address;
                    customer.PhoneNumber = userDto.PhoneNumber;
                    customer.CustomerTypeId = userDto.CustomerTypeId;
                    customer.Organization = userDto.Organization;
                    _customerRepository.Update(customer);
                }

                var finalResult = await _unitOfWork.CommitAsync();

                // Res
                var userResult = _mapper.Map<UserDto>(appUser);
                if (!string.IsNullOrEmpty(userResult.PersonalImagePath))
                {
                    userResult.PersonalImagePath = rootPath + userResult.PersonalImagePath;
                }

                _response.IsPassed = true;
                _response.Message = "Profile is updated successfully";
                _response.Data = userResult;

            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }
        public async Task<IResponseDTO> UpdateUserStatus(int loggedInUserId, int userId, string status, LocationDto locationDto)
        {
            try
            {
                bool isUnLocked = false;

                var appUser = await _userManager.FindByIdAsync(userId.ToString());
                if (appUser == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "User not found";
                    return _response;
                }
                if (appUser.Status == status)
                {
                    _response.IsPassed = false;
                    _response.Message = $"User is already {status}";
                    return _response;
                }


                // check if the admin activate or unlock the user
                isUnLocked = appUser.Status == UserStatusEnum.Locked.ToString() ? true : false;

                appUser.Status = status;
                if (status == UserStatusEnum.Active.ToString())
                {
                    appUser.AccessFailedCount = 0;
                }

                // Update the user in database
                var result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Message = $"Code: {result.Errors.FirstOrDefault().Code}, \n Description: {result.Errors.FirstOrDefault().Description}";
                    return _response;
                }

                // Send email when unlock
                if (status == UserStatusEnum.Active.ToString() && isUnLocked)
                {
                    // Token to reset tha pass
                    var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    resetPassToken = WebUtility.UrlEncode(resetPassToken);

                    appUser.ChangePassword = true;
                    await _userManager.UpdateAsync(appUser);

                    // send email
                    await _emailService.UnlockUserEmail(appUser.Email, resetPassToken);
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
        public async Task<GeneratedFile> ExportUsers(int loggedInUserId, bool isAdmin, int? pageIndex = null, int? pageSize = null, UserFilterDto filterDto = null)
        {
            try
            {
                // get users with roles
                IQueryable<ApplicationUser> appUsers = _userManager.Users.Include(x => x.UserRoles);
                if (isAdmin)
                {
                    var customerUser = await _userManager.FindByIdAsync(loggedInUserId.ToString());
                    var customer = _customerRepository.GetFirstOrDefault(x => x.Email == customerUser.Email);
                    appUsers = appUsers.Where(x => x.CustomerID == customer.Id);
                }
                else
                {
                    // Get Opian Users for Non Admin(SuperAdmin)
                    var opianRoles = _roleManager.Roles.Where(x => x.RoleType == 1).Select(x => x.Id).ToList();
                    var opianUserIds = _userRoleRepository.GetAll(x => opianRoles.Contains(x.RoleId)).Select(x => x.UserId).ToList();
                    appUsers = appUsers.Where(u => opianUserIds.Contains(u.Id));
                }

                if (filterDto != null)
                {

                    if (filterDto.RoleId != 0)
                    {
                        var role = _roleManager.Roles.FirstOrDefault(r => r.Id == filterDto.RoleId);
                        if (role != null)
                        {
                            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                            var usersInRoleIds = usersInRole.Select(x => x.Id);
                            appUsers = appUsers.Where(u => usersInRoleIds.Contains(u.Id));
                        }
                    }
                    if (!string.IsNullOrEmpty(filterDto.Email))
                    {
                        appUsers = appUsers.Where(u => u.Email.Trim().ToLower().Contains(filterDto.Email.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.PhoneNumber))
                    {
                        appUsers = appUsers.Where(u => u.PhoneNumber.Trim().ToLower().Contains(filterDto.PhoneNumber.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Status))
                    {
                        appUsers = appUsers.Where(u => u.Status.Trim().ToLower().Contains(filterDto.Status.Trim().ToLower()));
                    }
                    if (!string.IsNullOrEmpty(filterDto.Name))
                    {
                        appUsers = appUsers.Where(u => u.FirstName.Trim().ToLower().Contains(filterDto.Name.Trim().ToLower()) || u.LastName.Contains(filterDto.Name.Trim().ToLower()));
                    }


                    if (filterDto == null || (filterDto != null && string.IsNullOrEmpty(filterDto.SortProperty)))
                    {
                        appUsers = appUsers.OrderByDescending(x => x.Id);
                    }
                }

                //Check Sort Property
                if (filterDto != null && !string.IsNullOrEmpty(filterDto.SortProperty))
                {
                    appUsers = appUsers.OrderBy(
                     string.Format("{0} {1}", filterDto.SortProperty, filterDto.IsAscending ? "ASC" : "DESC"));
                }

                // Apply Pagination
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    appUsers = appUsers.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var dataList = _mapper.Map<List<ExportUserDto>>(appUsers.ToList());

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
        public async Task<IResponseDTO> UpdateBasicCustomerRole(string email, int planId)
        {
            // Basic Plan IDs: 2,3,4; Engineer Role ID: 6
            // If Customer's membership is Basic, the customer has Engineer Role
            // If Customer's membership is not Basic, this customer doesn't have the Engineer Role
            try
            {
                var customerUser = await _userManager.FindByEmailAsync(email);
                var customerUserRoles = _userRoleRepository.GetAll(x => x.UserId == customerUser.Id).Select(x => x.RoleId);
                if (planId == 2 || planId == 3 || planId == 4)  //Basic Plan
                {
                    if (!customerUserRoles.Contains(6))
                    {
                        _userRoleRepository.Add(new ApplicationUserRole()
                        {
                            UserId = customerUser.Id,
                            RoleId = 6
                        });
                    }
                }
                else
                {
                    if (customerUserRoles.Contains(6))
                    {
                        var oldUserRole = _userRoleRepository.GetAll(x => x.UserId == customerUser.Id && x.RoleId == 6);
                        _userRoleRepository.RemoveRange(oldUserRole);
                    }

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

    }


}


