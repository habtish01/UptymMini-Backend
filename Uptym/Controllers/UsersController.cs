using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uptym.Core.Interfaces;
using Uptym.Data.Enums;
using Uptym.DTO.Security;
using Uptym.DTO.Security.User;
using Uptym.Helpers;
using Uptym.Services.Security.User;
using Uptym.Validators.Security;

namespace Uptym.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(
            IUserService userService,
            IResponseDTO responseDTO,
            IHttpContextAccessor httpContextAccessor) : base(responseDTO, httpContextAccessor)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IResponseDTO> GetAllUsers(int? pageIndex = null, int? pageSize = null, [FromQuery] UserFilterDto filterDto = null)
        {
            _response = await _userService.GetAllUsers(LoggedInUserName, IsAdmin, ServerRootPath, pageIndex, pageSize, filterDto);
            return _response;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IResponseDTO> GetAllOpianUsers(int? pageIndex = null, int? pageSize = null, [FromQuery] UserFilterDto filterDto = null)
        {
            _response = await _userService.GetAllOpianUsers(ServerRootPath, pageIndex, pageSize, filterDto);
            return _response;
        }
        
        [HttpGet]
        public async Task<IResponseDTO> GetAllUsersAsDrp([FromQuery] UserFilterDto filterDto = null)
        {
            _response = await _userService.GetAllAsDrp(filterDto);
            return _response;
        }

        [AllowAnonymous]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IResponseDTO> Register([ModelBinder(BinderType = typeof(JsonModelBinder))] UserDto userDto)
        {
            // validate the user
            var validationResult = await (new UserValidator()).ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }

            // Set variables by the system
            userDto.CreatedBy = null;
            userDto.CreatedOn = DateTime.Now;
            userDto.Status = UserStatusEnum.Active.ToString();
            // Set relation variables with null to avoid unexpected EF errors
            //userDto.UserSubscriptionLevelName = null;

            var file = Request.Form.Files.Count() > 0 ? Request.Form.Files[0] : null;

            var result = await _userService.Register(userDto, file);

            return result;
        }

        [HttpGet]
        public async Task<IResponseDTO> GetUserDetails(int userId)
        {
            _response = await _userService.GetUserDetails(ServerRootPath, userId);
            return _response;
        }

        [AllowAnonymous]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IResponseDTO> CreateUser([ModelBinder(BinderType = typeof(JsonModelBinder))] UserDto userDto)
        {
            // validate the user
            var validationResult = await (new UserValidator()).ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }

            // Set variables by the system
            userDto.CreatedBy = LoggedInUserId;
            userDto.CreatedOn = DateTime.Now;
            userDto.Status = UserStatusEnum.Active.ToString();


            if (userDto.HealthFacilityId == null)
            {

                userDto.HealthFacilityId = userDto.CustomerId;
            }

            var file = Request?.Form?.Files.Count() > 0 ? Request?.Form?.Files[0] : null;
            var result = await _userService.CreateUser(LoggedInUserId, IsAdmin, userDto, file);

            return result;
        }

        [AllowAnonymous]
        [HttpPut, DisableRequestSizeLimit]
        public async Task<IResponseDTO> UpdateUser([ModelBinder(BinderType = typeof(JsonModelBinder))] UserDto userDto)
        {
            // validate the user
            var validationResult = await (new UserValidator()).ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }

            // Set variables by the system
            userDto.UpdatedBy = LoggedInUserId;
            userDto.UpdatedOn = DateTime.Now;

            var file = Request.Form.Files.Count() > 0 ? Request.Form.Files[0] : null;

            var result = await _userService.UpdateUser(ServerRootPath, userDto, file);
            return result;
        }

        [HttpPut]
        [AllowAnonymous]
        public async Task<IResponseDTO> UpdateUserStatus([FromBody] ChangeUserStatusParamsDto changeUserStatusParamsDto)
        {
            if (changeUserStatusParamsDto?.Status != UserStatusEnum.Active.ToString() &&
                changeUserStatusParamsDto?.Status != UserStatusEnum.Locked.ToString() &&
                changeUserStatusParamsDto?.Status != UserStatusEnum.NotActive.ToString())
            {
                _response.IsPassed = false;
                _response.Message = "Invalid status value";
                return _response;
            }
            if (changeUserStatusParamsDto?.Status == UserStatusEnum.Locked.ToString())
            {
                _response.IsPassed = false;
                _response.Message = "You can not lock the user account, only the system can";
                return _response;
            }

            _response = await _userService.UpdateUserStatus(LoggedInUserId, changeUserStatusParamsDto.UserId, changeUserStatusParamsDto.Status, changeUserStatusParamsDto.LocationDto);
            return _response;
        }

        [HttpPost]
        public async Task<IActionResult> ExportUsers(int? pageIndex = null, int? pageSize = null, [FromQuery] UserFilterDto filterDto = null)
        {
            var file = await _userService.ExportUsers(LoggedInUserId, IsAdmin, pageIndex, pageSize, filterDto);
            return File((byte[])file.Content, file.Extension, file.Name);
        }
    }
}