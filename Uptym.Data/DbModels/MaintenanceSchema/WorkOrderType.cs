using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("WorkOrderType", Schema = "Maintenance")]
    public class WorkOrderType : StaticLookup
    {
    }
}
