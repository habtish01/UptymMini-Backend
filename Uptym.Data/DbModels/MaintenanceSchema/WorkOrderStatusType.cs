using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("WorkOrderStatusType", Schema = "Maintenance")]
    public class WorkOrderStatusType : StaticLookup
    {
    }
}
