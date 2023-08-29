using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uptym.Controllers;
using Uptym.Core.Interfaces;
using Uptym.DTO.Configuration;
using Uptym.DTO.Configuration.ConfigurationAudit;
using Uptym.Services.Configuration.Configuration;
using Uptym.Validators.Configuration;

namespace Uptym.API.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ConfigurationsController : BaseController
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationsController(
            IConfigurationService configurationService,
            IResponseDTO responseDTO,
            IHttpContextAccessor httpContextAccessor) : base(responseDTO, httpContextAccessor)
        {
            _configurationService = configurationService;
        }

        [AllowAnonymous] 
        [HttpGet]
        public async Task<IResponseDTO> GetConfigurationDetails()
        {
            _response = await _configurationService.GetConfigurationDetails();
            return _response;
        }

        [HttpPut]
        public async Task<IResponseDTO> UpdateConfiguration([FromBody]ConfigurationDto configurationDto)
        {
            // validate
            var validationResult = await (new ConfigurationValidator()).ValidateAsync(configurationDto);
            if (!validationResult.IsValid)
            {
                _response.IsPassed = false;
                _response.Message = string.Join(",\n\r", validationResult.Errors.Select(e => e.ErrorMessage));
                _response.Data = null;
                return _response;
            }

            _response = await _configurationService.UpdateConfiguration(configurationDto, LoggedInUserId);
            return _response;
        }


        [HttpGet]
        public IResponseDTO GetAllConfigurationAudits(int? pageIndex, int? pageSize, [FromQuery] ConfigurationAuditFilterDto filterDto)
        {
            _response = _configurationService.GetAllConfigurationAudits(pageIndex, pageSize, filterDto);
            return _response;
        }
        

        [HttpPost]
        public IActionResult ExportConfigurationAudits(int? pageIndex = null, int? pageSize = null, [FromQuery] ConfigurationAuditFilterDto filterDto = null)
        {
            var file = _configurationService.ExportConfigurationAudits(pageIndex, pageSize, filterDto);
            return File((byte[])file.Content, file.Extension, file.Name);
        }
    }
}
