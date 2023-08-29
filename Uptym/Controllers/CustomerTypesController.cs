using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uptym.Core.Interfaces;
using Uptym.DTO.Subscription.CustomerType;
using Uptym.Helpers;
using Uptym.Services.Subscription.CustomerType;
using Uptym.Validators.Subscription;

namespace Uptym.Controllers
{
    [Authorize]
    public class CustomerTypesController : BaseController
    {
        public readonly ICustomerTypeService _customerTypeService;
        public CustomerTypesController(
            ICustomerTypeService customerTypeService,
            IResponseDTO response,
            IHttpContextAccessor httpContextAccessor) : base(response, httpContextAccessor)
        {
            _customerTypeService = customerTypeService;
        }

        [HttpGet]
        public IResponseDTO GetAllCustomerTypes(int? pageIndex = null, int? pageSize = null)
        {
            _response = _customerTypeService.GetAllCustomerTypes(pageIndex, pageSize);
            return _response;
        }

        [HttpGet]
        [AllowAnonymous]
        public IResponseDTO GetAllCustomerTypesAsDrp()
        {
            _response = _customerTypeService.GetAllCustomerTypesAsDrp();
            return _response;
        }

        [Authorize(Roles = "SuperAdmin")] 
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IResponseDTO> CreateCustomerType([ModelBinder(BinderType = typeof(JsonModelBinder))] CustomerTypeDto customerTypeDto)
        {
            // validate the user
            var validationResult = await (new CustomerTypeValidator()).ValidateAsync(customerTypeDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }

            // Set variables by the system
            customerTypeDto.CreatedBy = LoggedInUserId;
            customerTypeDto.CreatedOn = DateTime.Now;

            var result = await _customerTypeService.CreateCustomerType(customerTypeDto);

            return result;
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut, DisableRequestSizeLimit]
        public async Task<IResponseDTO> UpdateCustomerType([ModelBinder(BinderType = typeof(JsonModelBinder))] CustomerTypeDto customerTypeDto)
        {
            // validate the user
            var validationResult = await (new CustomerTypeValidator()).ValidateAsync(customerTypeDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }

            // Set variables by the system
            customerTypeDto.UpdatedBy = LoggedInUserId;
            customerTypeDto.UpdatedOn = DateTime.Now;

            var result = await _customerTypeService.UpdateCustomerType(customerTypeDto);
            return result;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete]
        public async Task<IResponseDTO> DeleteCustomerType(int customerTypeId)
        {

            _response = await _customerTypeService.DeleteCustomerType(customerTypeId);
            return _response;
        }



    }
}