using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Uptym.Core.Interfaces;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.DTO.Security;
using Uptym.DTO.Subscription.Customer;
using Uptym.Services.Security.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Uptym.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        public IResponseDTO _response;
        private IUserService _userService;

        public BaseController(IResponseDTO responseDTO, IHttpContextAccessor httpContextAccessor)
        {
            _response = responseDTO;
            _httpContextAccessor = httpContextAccessor;
        }

        public BaseController(IResponseDTO responseDTO, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _userService = userService;
            _response = responseDTO;
            _httpContextAccessor = httpContextAccessor;
        }

        public int LoggedInUserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == "userid")?.SingleOrDefault()?.Value != null)
                {
                    return int.Parse(_httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == "userid")?.SingleOrDefault()?.Value);
                }
                return 0;
            }
        }

        public bool IsSuperAdmin
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == "userid")?.SingleOrDefault()?.Value != null)
                {
                    return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "SuperAdmin") != null;
                }
                return false;
            }
        }

        public bool IsAdmin
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == "userid")?.SingleOrDefault()?.Value != null)
                {
                    return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "Admin") != null;
                }
                return false;
            }
        }
        public int? LoggedInCustomerId
        {
            get
            {
                if (LoggedInUserId > 0)
                {
                    var userResponse = _userService.GetUserDetails2(string.Empty, LoggedInUserId);
                    var userDto = (userResponse.Result.Data as UserDto);
                    if (userDto != null && userDto.CustomerId.HasValue)
                    {
                        return userDto.CustomerId;
                    }
                }
                return null;
            }
        }
        public int? LoggedInFacilityId
        {
            get
            {
                if (LoggedInUserId > 0)
                {
                    var userResponse = _userService.GetUserDetails2(string.Empty, LoggedInUserId);
                    var userDto = (userResponse.Result.Data as UserDto);
                    if (userDto != null && userDto.HealthFacilityId.HasValue)
                    {
                        return userDto.HealthFacilityId;
                    }
                }
                return null;
            }
        }

       

        public string LoggedInUserName { get { return _httpContextAccessor.HttpContext?.User?.Identity?.Name; } }
        public string ServerRootPath { get { return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; } }
        public string IP { get { return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.MapToIPv4().ToString(); } }


    }
}
