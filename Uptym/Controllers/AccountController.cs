using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uptym.Core.Interfaces;
using Uptym.DTO.Security;
using Uptym.DTO.Configuration;
using Uptym.Helpers;
using Uptym.Services.Security.Account;
using Uptym.Services.Security.User;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Uptym.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        public AccountController(
            IAccountService accountService,
            IResponseDTO response,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _accountService = accountService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IResponseDTO> Login([FromBody] LoginParamsDto loginParams)
        {
            _response = await _accountService.Login(ServerRootPath, loginParams);
            return _response;
        }


        [HttpPost]
        public async Task<IResponseDTO> ResetPassword([FromBody] ResetPasswordParamsDto resetPasswordParams)
        {
            _response = await _accountService.ResetPassword(ServerRootPath, resetPasswordParams);
            return _response;
        }

        [HttpPost]
        public async Task<IResponseDTO> ForgetPassword([FromBody] ForgetPassParamsDto forgetPassParamsDto)
        {
            _response = await _accountService.ForgetPassword(forgetPassParamsDto.Email.Trim(), forgetPassParamsDto.LocationDto);
            return _response;
        }

        [HttpPost]
        public async Task<IResponseDTO> UpdateLoggedInUserImage(int userId, string imagePath)
        {
            _response = await _accountService.UpdateUserImagePath(userId, imagePath);
            return _response;
        }


        [HttpPut, DisableRequestSizeLimit]
        public async Task<IResponseDTO> UpdateUserProfile([ModelBinder(BinderType = typeof(JsonModelBinder))] UserDto userDto)
        {
            // Set variables by the system
            userDto.UpdatedBy = LoggedInUserId;
            userDto.UpdatedOn = DateTime.Now;


            var file = Request?.Form?.Files?.Count() > 0 ? Request?.Form?.Files[0] : null;
            var result = await _userService.UpdateUser(ServerRootPath, userDto, file);
            return result;
        }


        [HttpPut]
        public async Task<IResponseDTO> ChangePassword([FromBody] ChangePasswordParamsDto userParams)
        {
            if (LoggedInUserId > 0 && LoggedInUserId != userParams.UserId)
            {
                _response.IsPassed = false;
                _response.Message = "You have only access to change your password";
                return _response;
            }
            if (userParams.CurrentPassword == userParams.NewPassword)
            {
                _response.IsPassed = false;
                _response.Message = "The new password should be different from the current password";
                return _response;
            }

            _response = await _accountService.ChangePassword(userParams);
            return _response;
        }


        [Authorize]
        [HttpGet]
        public async Task<IResponseDTO> GetLoggedInUserProfile(int userId)
        {
            if (LoggedInUserId != userId)
            {
                _response.IsPassed = false;
                _response.Message = "You don't have access to get the profile info";
                return _response;
            }

            _response = await _accountService.GetLoggedInUserProfile(ServerRootPath, userId);
            return _response;
        }


        [HttpGet]
        public IResponseDTO RefreshToken(int userId, string email)
        {
            _response.Data = _accountService.GenerateJSONWebToken(userId, email);
            _response.IsPassed = true;
            return _response;
        }


        [HttpGet]
        public async Task<IResponseDTO> CheckSessionExpiryDate(string Email, System.DateTime Date)
        {
            _response = await _accountService.CheckSessionExpiryDate(Email, Date);
            return _response;
        }


        [HttpPost]
        public async Task<IResponseDTO> ConfirmEmailAddress([FromBody] ConfirmEmailParamsDto confirmEmailParamsDto)
        {
            _response = await _accountService.ConfirmEmailAddress(confirmEmailParamsDto.Email, confirmEmailParamsDto.Token);
            return _response;
        }

       

        [HttpGet]
        public IResponseDTO GetWidgets(int? pageIndex = null, int? pageSize = null)
        {
            _response = _accountService.GetWidgets(pageIndex, pageSize, LoggedInUserId);
            return _response;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IResponseDTO> AddWidgets([FromBody] UserPreferenceDto[] userPrefDto = null)
        {

            _response = await _accountService.AddWidgets(userPrefDto, LoggedInUserId);
            return _response;
        }


    }
}
