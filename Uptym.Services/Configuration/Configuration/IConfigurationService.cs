using System.Threading.Tasks;
using Uptym.Core.Common;
using Uptym.Core.Interfaces;
using Uptym.DTO.Configuration;
using Uptym.DTO.Configuration.ConfigurationAudit;
using Uptym.Repositories.Configuration.Configuration;
using Uptym.Services.Generics;

namespace Uptym.Services.Configuration.Configuration
{
    public interface IConfigurationService : IGService<ConfigurationDto, Data.DbModels.ConfigurationSchema.Configuration, IConfigurationRepository>
    {
        Task<IResponseDTO> GetConfigurationDetails();
        Task<IResponseDTO> UpdateConfiguration(ConfigurationDto configurationDto, int loggedInUserId);
        Task<IResponseDTO> GetTimeToChangePassword();
        Task<IResponseDTO> TimeToSessionTimeOut();
        IResponseDTO GetAllConfigurationAudits(int? pageIndex = null, int? pageSize = null, ConfigurationAuditFilterDto filterDto = null);
        GeneratedFile ExportConfigurationAudits(int? pageIndex = null, int? pageSize = null, ConfigurationAuditFilterDto filterDto = null);
    }
}
