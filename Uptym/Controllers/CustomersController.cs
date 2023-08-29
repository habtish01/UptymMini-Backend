using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uptym.Core.Interfaces;
using Uptym.Data.Enums;
using Uptym.DTO.Subscription.Customer;
using Uptym.Helpers;
using Uptym.Services.Security.User;
using Uptym.Services.Subscription.Customer;
using Uptym.Validators.Subscription;


namespace Uptym.Controllers
{
    [Authorize]
    public class CustomersController : BaseController
    {
        public readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public CustomersController(
            ICustomerService customerService,
            IResponseDTO response,
            IUserService userService,

            IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor, userService)
        {
            _customerService = customerService;
            _userService = userService;

        }

        [AllowAnonymous]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IResponseDTO> Register([ModelBinder(BinderType = typeof(JsonModelBinder))] CustomerDto customerDto)
        {
            // validate the customer
            var validationResult = await (new CustomerValidator()).ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }
            customerDto.CreatedBy = null;
            customerDto.CreatedOn = DateTime.Now;
            //customerDto.Status = UserStatusEnum.Active.ToString();  

            var file = Request?.Form?.Files.Count() > 0 ? Request?.Form?.Files[0] : null;
            _response = await _customerService.Register(customerDto, file);
            return _response;
        }

        [HttpGet]
        public IResponseDTO GetAllCustomers(int? pageIndex = null, int? pageSize = null, [FromQuery] CustomerFilterDto filterDto = null)
        {
            _response = _customerService.GetAll(pageIndex, pageSize, filterDto);
            return _response;
        }

        [HttpGet]
        public IResponseDTO GetAllAsDrp([FromQuery] CustomerFilterDto filterDto = null)
        {
            _response = _customerService.GetAllAsDrp(filterDto);
            return _response;
        }

        [HttpGet]
        public async Task<IResponseDTO> GetCustomerDetails(int customerId)
        {
            _response = await _customerService.GetCustomerDetails(customerId);
            return _response;
        }

        [HttpGet]
        public async Task<IResponseDTO> GetCurrentCustomerDetails()
        {

            int? customerId = LoggedInCustomerId;
            if (customerId != null)
                _response = await _customerService.GetCustomerDetails((int)customerId);
            else
            {
                _response.IsPassed = false;
                _response.Message = "User does not have Customer";
                _response.Data = null;
            }
            return _response;
        }
        [HttpGet]
        public async Task<IResponseDTO> GetCustomerDetailsByUserId()
        {
            _response = await _customerService.GetCustomerDetailsByUserId(LoggedInUserId);
            return _response;
        }

        [HttpGet]
        public async Task<IResponseDTO> GetCustomerMembershipInfo()
        {
            _response = await _customerService.GetCustomerMembershipInfo(LoggedInUserId);
            return _response;
        }


        [HttpPost, DisableRequestSizeLimit]
        public async Task<IResponseDTO> CreateCustomer([ModelBinder(BinderType = typeof(JsonModelBinder))] CustomerDto customerDto)
        {
            // validate the user
            var validationResult = await (new CustomerValidator()).ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }

            // Set variables by the system
            customerDto.CreatedBy = LoggedInUserId;
            customerDto.CreatedOn = DateTime.Now;
            customerDto.Status = UserStatusEnum.Active.ToString();

            var file = Request?.Form?.Files.Count() > 0 ? Request?.Form?.Files[0] : null;
            var result = await _customerService.CreateCustomer(customerDto, file);

            return result;
        }

        [HttpPut, DisableRequestSizeLimit]
        public async Task<IResponseDTO> UpdateCustomer([ModelBinder(BinderType = typeof(JsonModelBinder))] CustomerDto customerDto)
        {
            // validate the user
            var validationResult = await (new CustomerValidator()).ValidateAsync(customerDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }

            // Set variables by the system
            customerDto.UpdatedBy = LoggedInUserId;
            customerDto.UpdatedOn = DateTime.Now;

            var file = Request.Form.Files.Count() > 0 ? Request.Form.Files[0] : null;

            var result = await _customerService.UpdateCustomer(ServerRootPath, customerDto, file);
            return result;
        }

        [HttpPut]
        public async Task<IResponseDTO> UpdateCustomerReminder([ModelBinder(BinderType = typeof(JsonModelBinder))] CustomerDto customerDto)
        {
            var result = await _customerService.UpdateCustomerReminder(LoggedInUserId, customerDto.ReminderDays);
            return result;
        }

        [HttpPut]
        public async Task<IResponseDTO> UpdateCustomerStatus([FromBody] ChangeCustomerStatusParamsDto changeCustomerStatusParamsDto)
        {
            if (changeCustomerStatusParamsDto?.Status != UserStatusEnum.Active.ToString() &&
                changeCustomerStatusParamsDto?.Status != UserStatusEnum.Locked.ToString() &&
                changeCustomerStatusParamsDto?.Status != UserStatusEnum.NotActive.ToString())
            {
                _response.IsPassed = false;
                _response.Message = "Invalid status value";
                return _response;
            }
            if (changeCustomerStatusParamsDto?.Status == UserStatusEnum.Locked.ToString())
            {
                _response.IsPassed = false;
                _response.Message = "You can not lock the user account, only the system can";
                return _response;
            }

            _response = await _customerService.UpdateCustomerStatus(changeCustomerStatusParamsDto.CustomerId, changeCustomerStatusParamsDto.Status, changeCustomerStatusParamsDto.LocationDto);
            return _response;
        }

        [HttpPost]
        public async Task<IActionResult> ExportCustomers(int? pageIndex = null, int? pageSize = null, [FromQuery] CustomerFilterDto filterDto = null)
        {
            var file = await _customerService.ExportUsers(pageIndex, pageSize, filterDto);
            return File((byte[])file.Content, file.Extension, file.Name);
        }


    }
}