using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Uptym.Core.Common;
using Uptym.Core.Interfaces;
using Uptym.Data.Enums;
using Uptym.DTO.Common;
using Uptym.DTO.Security;
using Uptym.DTO.Security.User;

namespace Uptym.Services.Security.User
{
    public interface IUserService
    {
        Task<IResponseDTO> CreateUser(int loggedInUserId, bool isAdmin, UserDto userDto, IFormFile file);
        Task<GeneratedFile> ExportUsers(int loggedInUserId, bool isAdmin, int? pageIndex = null, int? pageSize = null, UserFilterDto filterDto = null);
        Task<IResponseDTO> GetAllAsDrp(UserFilterDto filterDto = null);
        Task<IResponseDTO> GetAllOpianUsers(string rootPath, int? pageIndex = null, int? pageSize = null, UserFilterDto filterDto = null);
        Task<IResponseDTO> GetAllUsers(string loggedInUserName, bool isAdmin, string rootPath, int? pageIndex = null, int? pageSize = null, UserFilterDto filterDto = null);
        Task<IResponseDTO> GetUserDetails(string rootPath, int userId);
        Task<IResponseDTO> Register(UserDto userDto, IFormFile file);
        Task<IResponseDTO> UpdateUser(string rootPath, UserDto userDto, IFormFile file);
        Task<IResponseDTO> UpdateUserStatus(int loggedInUserId, int userId, string status, LocationDto locationDto);
        List<string> GetRoles(int userId);
        Task<IResponseDTO> GetUserDetails2(string rootPath, int userId);
        Task<IResponseDTO> GetUserByEmailAsync(string email);
        Task<IResponseDTO> GetUserEmails(int? userId,  int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum);
        Task<IResponseDTO> GetUsersEmails(int? userId, int? WorkorderID);
        Task<IResponseDTO> GetUserPhoneNumbers(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum);
        Task<IResponseDTO> GetManagerNames(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum);
        Task<IResponseDTO> GetEngineerNames(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum);
        Task<IResponseDTO> GetFacilityNames(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum);
        Task<IResponseDTO> GetManagerName(int? userId, int? facilityId, int? customerId, ApplicationRolesEnum rolesEnum);
        Task<IResponseDTO> UpdateBasicCustomerRole(string email, int planId);

        Task<IResponseDTO> GetSingleSite(int healthFacilityId);

    }
}