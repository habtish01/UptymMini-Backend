using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("Priority", Schema ="Maintenance")]
    public class Priority : StaticLookup
    {
    }
}
