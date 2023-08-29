using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Uptym.Core.Interfaces;
using Uptym.Data.DataContext;
using Uptym.Data.Enums;
using Uptym.DTO.Security;
using Uptym.DTO.Configuration;
using Uptym.Repositories.Security.UserRole;
using Uptym.Repositories.Security.UserTransactionHistory;
using Uptym.Repositories.UOW;
using Uptym.Services.Global.SendEmail;
using Uptym.Services.Global.UploadFiles;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Uptym.Data.BaseModeling;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Uptym.Repositories.Configuration.Configuration;
using Uptym.DTO.Common;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Repositories.Subscription.Membership;
using Uptym.Repositories.Subscription.Customer;
using Uptym.Repositories.Metadata;
using Uptym.Data.DbModels.ConfigurationSchema;
using Uptym.DTO.Metadata;
using Uptym.Data.DbModels.MetadataSchema;

namespace Uptym.Services.Security.Account
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationRepository _configurationRepository;
        private readonly IHealthFacilityTypeRepository _healthFacilityTypeRepository;
        private readonly IHealthFacilityRepository _healthFacilityRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly IResponseDTO _response;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly IUploadFilesService _uploadFilesService;
        private readonly IEmailService _emailService;
        private readonly IUserTransactionHistoryRepository _userTransactionHistoryRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMembershipRepository _membershipRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserPreferenceRepository _userPreferenceRepository;
        private readonly IWidgetRepository _widgetRepository;

        public AccountService(
          IUnitOfWork<AppDbContext> unitOfWork,
          IConfiguration configuration,
          IMapper mapper,
          UserManager<ApplicationUser> userManager,
          IPasswordHasher<ApplicationUser> passwordHasher,
          IResponseDTO responseDTO,
          IUploadFilesService uploadFilesService,
          IUserTransactionHistoryRepository userTransactionHistoryRepository,
          IEmailService emailService,
          IConfigurationRepository configurationRepository,
          IUserRoleRepository userRoleRepository,
          ICustomerRepository customerRepository,
          IMembershipRepository membershipRepository,
          RoleManager<ApplicationRole> roleManager,
          IUserPreferenceRepository userPreferenceRepository,
          IHealthFacilityTypeRepository healthFacilityTypeRepository,
          IHealthFacilityRepository healthFacilityRepository,
          IWidgetRepository widgetRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _response = responseDTO;
            _unitOfWork = unitOfWork;
            _uploadFilesService = uploadFilesService;
            _emailService = emailService;
            _configurationRepository = configurationRepository;
            _userTransactionHistoryRepository = userTransactionHistoryRepository;
            _userRoleRepository = userRoleRepository;
            _customerRepository = customerRepository;
            _membershipRepository = membershipRepository;
            _roleManager = roleManager;
            _userPreferenceRepository = userPreferenceRepository;
            _widgetRepository = widgetRepository;
            _healthFacilityTypeRepository = healthFacilityTypeRepository;
            _healthFacilityRepository = healthFacilityRepository;
        }
        public async Task<IResponseDTO> Login(string rootPath, LoginParamsDto loginParams)
        {
            try
            {
                var config = await _configurationRepository.GetFirstAsync();
                var appUser = await _userManager.FindByEmailAsync(loginParams.Email);
               
                if (appUser == null)
                {
                    _response.Message = "Email is not found";
                    _response.IsPassed = false;
                    return _response;
                }

                if (appUser.Status == UserStatusEnum.Locked.ToString())
                {
                    _response.Message = "Your Account is locked. Please contact your administration";
                    _response.IsPassed = false;
                    return _response;
                }

                if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Message = "Your Account is disabled. Please contact your administration";
                    _response.IsPassed = false;
                    return _response;
                }
                if (appUser.CustomerID != null)
                {
                    var trialEndDate = _membershipRepository.GetAll().Where(x => x.CustomerId == appUser.CustomerID).FirstOrDefault().EndDate;
                  
                    int result = DateTime.Compare(DateTime.Today.Date, trialEndDate.Date);
                    if (result >= 0)
                    {
                        _response.Message = "Your Account is Expired. Please Subscribe Another Package";
                        _response.IsPassed = false;
                        return _response;
                    }

                }
              

                var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Email == appUser.Email);
                if (appUser != null &&
                    _passwordHasher.VerifyHashedPassword(appUser, appUser.PasswordHash, loginParams.Password) !=
                    PasswordVerificationResult.Success)
                {
                    appUser.AccessFailedCount += 1;
                    await _userManager.UpdateAsync(appUser);

                    if (appUser.AccessFailedCount == config.AccountLoginAttempts)
                    {
                        // lock the account
                        appUser.Status = UserStatusEnum.Locked.ToString();
                        if (customer != null)
                        {
                            customer.Status = UserStatusEnum.Locked.ToString();
                            _customerRepository.Update(customer);
                            await _unitOfWork.CommitAsync();
                        }
                        await _userManager.UpdateAsync(appUser);

                        // send email here
                        await _emailService.AfterAccountIsDisabled(appUser.Email, DateTime.Now, loginParams.LocationDto.IP, loginParams.LocationDto.CountryName);

                        _response.Message = $"You have unsuccessfully logged into your account {appUser.AccessFailedCount} times and your account has been locked.  Please contact your administration in order to have your account unlocked.";
                        _response.IsPassed = false;
                        return _response;
                    }

                    // send email here
                    //await _emailService.WrongPasswordAttempt(appUser.Email, DateTime.Now, loginParams.LocationDto.IP, loginParams.LocationDto.CountryName);

                    _response.Message = $"Invalid password, you have {config.AccountLoginAttempts - appUser.AccessFailedCount} Attempts then your account will be locked.";
                    _response.IsPassed = false;
                    return _response;
                }

                if (!appUser.EmailConfirmed)
                {
                    _response.Message = "Please check your email inbox for an email from the system administrator in order to confirm your email. If you cannot find the email, please contact the application administrator.";
                    _response.IsPassed = false;
                    return _response;
                }

                if (DateTime.Now.Date >= appUser.NextPasswordExpiryDate.Date)
                {
                    _response.Message = "Your password is expired";
                    _response.IsPassed = false;
                    return _response;
                }

                var authorizedUserDto = _mapper.Map<AuthorizedUserDto>(appUser);

                // Check the Customer's membership
                if (customer != null)
                {
                    if (customer.Status == UserStatusEnum.Locked.ToString())
                    {
                        _response.Message = "Your Customer Account is locked. Please contact your administration";
                        _response.IsPassed = false;
                        return _response;
                    }

                    if (customer.Status == UserStatusEnum.NotActive.ToString())
                    {
                        _response.Message = "Your Customer Account is disabled. Please contact your administration";
                        _response.IsPassed = false;
                        return _response;
                    }

                    var membership = _membershipRepository.GetAll(x => x.CustomerId == customer.Id &&
                        x.Status == MembershipStatusEnum.Active.ToString()).Include(x => x.Plan).FirstOrDefault();
                    if (membership == null)
                    {
                        _response.Message = "Your Membership is not Active. Please contact system administrator";
                        _response.IsPassed = false;
                        return _response;
                    }
                    else
                    {
                        authorizedUserDto.Expiration = membership.EndDate;
                        authorizedUserDto.ExtraEndDate = membership.ExtraEndDate;
                        // Now < EndDate < ExtraEndDate
                        if (membership.EndDate > DateTime.Now)
                        {
                            authorizedUserDto.ExpirationStatus = ExpirationStatusEnum.Active.ToString();
                        }
                        else if (membership.ExtraEndDate > DateTime.Now)
                        {
                            authorizedUserDto.ExpirationStatus = ExpirationStatusEnum.Expired.ToString();
                        }
                        else
                        {
                            authorizedUserDto.ExpirationStatus = ExpirationStatusEnum.ExtraExpired.ToString();
                        }
                    }
                }


                if (appUser.ChangePassword)
                {
                    var resetPassToken = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    // encode the token
                    authorizedUserDto.Token = WebUtility.UrlEncode(resetPassToken);
                }
                else
                {
                    authorizedUserDto.Token = GenerateJSONWebToken(appUser.Id, appUser.UserName);
                }

                authorizedUserDto.UserRoles = new List<string>();

                // get user roles
                authorizedUserDto.UserRoles = GetRoles(appUser.Id);

                authorizedUserDto.UserWidgets = new List<int>();
                //get user Widgets
                authorizedUserDto.UserWidgets = GetUserWidgets(appUser.Id);

                if (!string.IsNullOrEmpty(authorizedUserDto.PersonalImagePath))
                {
                    authorizedUserDto.PersonalImagePath = rootPath + authorizedUserDto.PersonalImagePath;
                }

                authorizedUserDto.LoggedInCount = _userTransactionHistoryRepository
                    .GetAll(x => x.UserId == appUser.Id && x.UserTransactionTypeId == (int)UserTransactionTypesEnum.Login).Count();
                // Add Transaction here
                await _userTransactionHistoryRepository.AddAsync(new UserTransactionHistory
                {
                    Location = _mapper.Map<Location>(loginParams.LocationDto),
                    CreatedBy = appUser.Id,
                    CreatedOn = DateTime.Now,
                    UserTransactionTypeId = (int)UserTransactionTypesEnum.Login,
                    Description = "User logged in successfully",
                    UserId = appUser.Id,
                    From = "Unauthenticated status",
                    To = "authenticated status",
                    IsDeleted = false,
                });

                var finalResult = await _unitOfWork.CommitAsync();
                if (finalResult == 0)
                {
                    _response.IsPassed = false;
                    _response.Message = "Faild to login";
                    return _response;
                }

                // in case user logged in successfully, reset AccessFailedCount
                if (appUser.AccessFailedCount > 0)
                {
                    appUser.AccessFailedCount = 0;
                    await _userManager.UpdateAsync(appUser);
                }

                _response.IsPassed = true;
                _response.Message = "You are logged in successfully.";
                _response.Data = authorizedUserDto;
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
                return _response;
            }

            return _response;
        }


        public async Task<IResponseDTO> ResetPassword(string rootPath, ResetPasswordParamsDto resetPasswordParams)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(resetPasswordParams.Email.Trim());
                if (appUser == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid Email";
                    return _response;
                }

                if (appUser.Status == UserStatusEnum.Locked.ToString())
                {
                    _response.Message = "Your Account is locked. Please contact your administration";
                    _response.IsPassed = false;
                    return _response;
                }

                if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Message = "Your Account is disabled. Please contact your administration";
                    _response.IsPassed = false;
                    return _response;
                }

                // appUser.IsPasswordSet = true;
                var result = await _userManager.ResetPasswordAsync(appUser, resetPasswordParams.Token, resetPasswordParams.NewPassword);
                if (!result.Succeeded)
                {
                    _response.IsPassed = false;
                    _response.Message = $"{result.Errors.FirstOrDefault().Description}";
                    return _response;
                }

                var daysToChnagePass = _configurationRepository.GetFirst().NumOfDaysToChangePassword;
                appUser.NextPasswordExpiryDate = DateTime.Now.AddDays(daysToChnagePass);
                appUser.ChangePassword = false;
                appUser.EmailConfirmed = true;
                appUser.EmailVerifiedDate = DateTime.Now;

                await _userManager.UpdateAsync(appUser);

                // send email
                await _emailService.AfterResetPassword(resetPasswordParams.Email.Trim());

                _response.IsPassed = true;
                _response.Message = "Your password is resetted successfully";
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
                return _response;
            }

            return _response;
        }
        public async Task<IResponseDTO> ForgetPassword(string email, LocationDto locationDto)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(email);
                if (appUser == null)
                {
                    _response.IsPassed = false;
                    _response.Message = "Invalid Email";
                    return _response;
                }

                if (appUser.Status == UserStatusEnum.Locked.ToString())
                {
                    _response.Message = "Your Account is locked. Please contact your administration";
                    _response.IsPassed = false;
                    return _response;
                }

                if (appUser.Status == UserStatusEnum.NotActive.ToString())
                {
                    _response.Message = "Your Account is disabled. Please contact your administration";
                    _response.IsPassed = false;
                    return _response;
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

                // encode the token
                string validToken = HttpUtility.UrlEncode(token);

                // send email
                await _emailService.RequestToResetPassword(email, validToken);

                _response.IsPassed = true;
                _response.Message = "Done";
                _response.Data = validToken;
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
                return _response;
            }

            return _response;
        }
        public async Task<IResponseDTO> ChangePassword(ChangePasswordParamsDto userParams)
        {
            var appUser = await _userManager.FindByEmailAsync(userParams.Email.Trim());
            if (appUser == null)
            {
                _response.IsPassed = false;
                _response.Message = "Invalid email or password";
                return _response;
            }

            if (appUser.Status == UserStatusEnum.Locked.ToString())
            {
                _response.Message = "Your Account is locked. Please contact your administration";
                _response.IsPassed = false;
                return _response;
            }

            if (appUser.Status == UserStatusEnum.NotActive.ToString())
            {
                _response.Message = "Your Account is disabled. Please contact your administration";
                _response.IsPassed = false;
                return _response;
            }

            var result = await _userManager.ChangePasswordAsync(appUser, userParams.CurrentPassword, userParams.NewPassword);
            if (!result.Succeeded)
            {
                _response.IsPassed = false;
                _response.Message = $"Code: {result.Errors.FirstOrDefault().Code} {result.Errors.FirstOrDefault().Description}";
                return _response;
            }


            var daysToChnagePass = _configurationRepository.GetFirst().NumOfDaysToChangePassword;
            appUser.NextPasswordExpiryDate = DateTime.Now.AddDays(daysToChnagePass);

            // Update the user and commit
            await _userManager.UpdateAsync(appUser);

            // Send email
            //await _emailService.AfterPasswordChanges(userParams.Email.Trim(), DateTime.Now, userParams.LocationDto.IP, userParams.LocationDto.CountryName);

            _response.IsPassed = true;
            _response.Message = "Done";
            return _response;
        }

        public async Task<IResponseDTO> GetLoggedInUserProfile(string rootPath, int userId)
        {
            var appUser = await _userManager.Users.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == userId);

            var userDetailsDto = _mapper.Map<UserDto>(appUser);
            var userRoles = await _userManager.GetRolesAsync(appUser);
            userDetailsDto.UserRoles = userRoles.ToList();
            userDetailsDto.UserRoleLevels = new List<int>();
           var Department =  await _healthFacilityTypeRepository.GetFirstOrDefaultAsync(x => x.Id == userDetailsDto.HealthFacilityTypeId);
            var Section = await _healthFacilityRepository.GetFirstOrDefaultAsync(x => x.Id == userDetailsDto.HealthFacilityId);
          
            if (Section != null)
            {
                userDetailsDto.HealthFacilityName = Section.Name;
            }
            if (Department != null)
            {
               
                userDetailsDto.HealthFacilityTypeName = Department.Name;
            }
            else
            {
                userDetailsDto.HealthFacilityName = "-";
                userDetailsDto.HealthFacilityTypeName = "-";
            }
            if (Department != null)
            {
                userDetailsDto.HealthFacilityTypeName = Department.Name;
            }
            else
            {
                userDetailsDto.HealthFacilityName = "-";
                userDetailsDto.HealthFacilityTypeName = "-";
            }


            for (int i = 0; i < appUser.UserRoles.Count; i++)
            {
                var roleId = appUser.UserRoles.ElementAt(i).RoleId;
                userDetailsDto.UserRoleLevels.Add(roleId);

            }

            foreach (string userRole in userRoles.ToList())
            {
                if (userRole == "Admin")
                {
                    var customer = await _customerRepository.GetFirstOrDefaultAsync(x => x.Email == appUser.Email);
                    userDetailsDto.Organization = customer.Organization;
                    userDetailsDto.CustomerTypeId = customer.CustomerTypeId;
                }
            }

            if (!string.IsNullOrEmpty(userDetailsDto.PersonalImagePath))
            {
                userDetailsDto.PersonalImagePath = rootPath + userDetailsDto.PersonalImagePath;
            }
            userDetailsDto.LoggedInUserCount = _userTransactionHistoryRepository
                   .GetAll(x => x.UserId == appUser.Id && x.UserTransactionTypeId == (int)UserTransactionTypesEnum.Login).Count();
            _response.IsPassed = true;
            _response.Data = userDetailsDto;
            return _response;
        }
        public async Task<IResponseDTO> UpdateUserImagePath(int userId, string imagePath)
        {
            var appUser = await _userManager.FindByIdAsync(userId.ToString());
            appUser.PersonalImagePath = imagePath;

            var result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                _response.IsPassed = false;
                _response.Message = $"Code: {result.Errors.FirstOrDefault().Code} {result.Errors.FirstOrDefault().Description}";
                return _response;
            }

            _response.IsPassed = true;
            _response.Message = "Done";
            _response.Data = null;

            return _response;
        }
        public async Task<IResponseDTO> CheckSessionExpiryDate(string Email, DateTime VisitSetPasswordPageDate)
        {
            var appUser = await _userManager.FindByEmailAsync(Email);

            if (appUser == null)
            {
                _response.Message = "There is no user with the specified email";
                _response.Data = null;
                _response.IsPassed = false;
                return _response;

            }

            //session time out !
            var dateOfMailSent = appUser.CreatedOn;
            if (VisitSetPasswordPageDate == null)
            {
                _response.Data = null;
                _response.IsPassed = false;
                _response.Message = "Inavild Date";
                return _response;
            }


            _response.IsPassed = true;
            _response.Message = "Session time is allowed";
            return _response;
        }
        public string GenerateJSONWebTokenForResetPass(int userId, string userName)
        {
            //var signingKey = Convert.FromBase64String(_configuration[]);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.
                  GetBytes(_configuration.GetSection("Jwt:Key").Value));

            var resultFromConfigurationTable = _configurationRepository.GetFirst().TimeToSessionTimeOut;
            double expiryDuration = (Convert.ToDouble(resultFromConfigurationTable));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim>() {
                new Claim("userid", userId.ToString()),
                //new Claim("role", role),
                new Claim(ClaimTypes.NameIdentifier, userName)
            }),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
        public async Task<IResponseDTO> ConfirmEmailAddress(string email, string token)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(email.Trim().ToLower());

                if (appUser == null)
                {
                    _response.Message = "Invalid user email";
                    _response.IsPassed = false;
                    return _response;
                }

                var result = await _userManager.ConfirmEmailAsync(appUser, token);
                if (!result.Succeeded)
                {
                    _response.Message = $"{result.Errors.FirstOrDefault().Description}";
                    _response.IsPassed = false;
                    return _response;
                }

                _response.Message = "Ok";
                _response.IsPassed = true;
            }
            catch (Exception ex)
            {
                _response.IsPassed = false;
                _response.Message = $"Error: {ex.Message} Details: {ex.InnerException.Message}";
                return _response;
            }

            return _response;
        }

        public string GenerateJSONWebToken(int userId, string userName)
        {
            var config = _configurationRepository.GetFirst();
            var signingKey = Convert.FromBase64String(_configuration["Jwt:Key"]);
            //var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);
            var expiryDuration = config.PasswordExpiryTime;

            var claims = new List<Claim>
            {
                new Claim("userid", userId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName)
            };

            var user = _userManager.FindByIdAsync(userId.ToString()).Result;
            var roles = _userManager.GetRolesAsync(user).Result;
            foreach (var item in roles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, item);
                claims.Add(roleClaim);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
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

        private List<int> GetUserWidgets(int userId)
        {
            List<int> result = new List<int>();
            try
            {
                var widgetIds = _userPreferenceRepository.GetAll(x => x.UserId == userId).Select(x => x.WidgetId);
                var widgetTags = _widgetRepository.GetAll().Where(x => widgetIds.Contains(x.Id)).Select(x => x.WidgetTag);

                result = widgetTags.ToList();
            }
            catch (Exception ex)
            {
                var message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
            }

            return result;
        }

        private List<int> GetWidgets(int userId)
        {
            List<int> result = new List<int>();
            try
            {
                var widgetIds = _userPreferenceRepository.GetAll(x => x.UserId == userId).Select(x => x.WidgetId);
                var widgetTags = _widgetRepository.GetAll().Where(x => widgetIds.Contains(x.Id)).Select(x => x.WidgetTag);

                result = widgetTags.ToList();
            }
            catch (Exception ex)
            {
                var message = $"Error: {ex.Message} Details: {ex.InnerException?.Message}";
            }

            return result;
        }


        public async Task<IResponseDTO> AddWidgets(UserPreferenceDto[] userPrefDto = null, int loggedInUserId = 0)
        {
            try
            {

                IList<UserPreference> queryables = _userPreferenceRepository.GetAll(x => x.UserId == loggedInUserId).ToList();

                foreach (UserPreferenceDto item in userPrefDto)
                {
                    UserPreference userref = new UserPreference();
                    userref.PreferenceKey= item.PreferenceKey.ToString();
                    userref.PreferenceType = item.PreferenceValue.ToString();
                    userref.PreferenceValue = item.PreferenceValue.ToString();
                    userref.UserId = loggedInUserId;
                    userref.WidgetId = item.WidgetId;
                    ////Add Last status
                    //item.LastStatus = specimenTestTracking.SpecimenStatusId;
                    //_requestedTestRepository.Update(item);

                    _userPreferenceRepository.Add(userref);
                }


                _unitOfWork.Commit();

                _response.Data = null;
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



        public IResponseDTO GetWidgets(int? pageIndex = null, int? pageSize = null, int loggedInUserId = 0)
               
        {
            try
            {


                
                IList<UserPreference> queryables = _userPreferenceRepository.GetAll(x => x.UserId == loggedInUserId).ToList();

                IList<Widget> widgQuerybles = _widgetRepository.GetAll().ToList();

                var datalist = widgQuerybles.ToList();
                    
                    

                

                _response.Data = datalist;
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

    }
}
