using System;
using System.Threading.Tasks;
using Uptym.Core.Interfaces;
using Uptym.DTO.Common;
using Uptym.DTO.Security;
using Uptym.DTO.Configuration;

namespace Uptym.Services.Security.Account
{
    public interface IAccountService
    {
        string GenerateJSONWebToken(int userId, string userName);
        Task<IResponseDTO> Login(string rootPath, LoginParamsDto loginParams);
        Task<IResponseDTO> ResetPassword(string rootPath, ResetPasswordParamsDto resetPasswordParams);
        Task<IResponseDTO> ForgetPassword(string email, LocationDto locationDto);
        Task<IResponseDTO> ChangePassword(ChangePasswordParamsDto userParams);
        Task<IResponseDTO> GetLoggedInUserProfile(string rootPath, int userId);
        Task<IResponseDTO> UpdateUserImagePath(int userId, string imagePath);
        Task<IResponseDTO> CheckSessionExpiryDate(string Email, DateTime VisitSetPageDate);
        string GenerateJSONWebTokenForResetPass(int userId, string userName);
        Task<IResponseDTO> ConfirmEmailAddress(string email, string token);
        Task<IResponseDTO> AddWidgets(UserPreferenceDto[] userPrefDto = null, int  loggedInUserId = 0);
       
        IResponseDTO GetWidgets(int? pageIndex = null, int? pageSize = null, int loggedInUserId=0);

    }
}
