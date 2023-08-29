using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;

namespace Uptym.Data.DbModels.ConfigurationSchema
{
    [Table("UserPreferences", Schema = "Configuration")]
    public class UserPreference
    {
        public int Id { get; set; }
        public string PreferenceType { get; set; }
        public string PreferenceKey { get; set; }
        public string PreferenceValue { get; set; }
        public int WidgetId { get; set; }
        public int UserId { get; set; }
        public virtual Widget Widget { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
