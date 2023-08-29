using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.ConfigurationSchema
{
    [Table("ConfigurationAudits", Schema = "Configuration")]
    public class ConfigurationAudit : AuditEntity
    {
        public int ConfigurationId { get; set; }

        // User Managment
        public int NumOfDaysToChangePassword { get; set; }
        public int AccountLoginAttempts { get; set; }
        public int PasswordExpiryTime { get; set; }
        public double UserPhotosize { get; set; }
        public double AttachmentsMaxSize { get; set; }
        public int TimesCountBeforePasswordReuse { get; set; }
        public int TimeToSessionTimeOut { get; set; }
        public int TrialPeriodDays { get; set; }
        public int ReminderDays { get; set; }

        public virtual Configuration Configuration { get; set; }
    }
}
