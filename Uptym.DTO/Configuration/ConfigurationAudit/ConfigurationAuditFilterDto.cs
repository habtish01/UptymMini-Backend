using Uptym.DTO.Common;
using System;

namespace Uptym.DTO.Configuration.ConfigurationAudit 
{
    public class ConfigurationAuditFilterDto : BaseFilterDto
    {
        public DateTime? DateOfAction { get; set; }
        public string Action { get; set; }
        public int CreatedBy { get; set; }
    }
}
