using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.DbModels.SubscriptionSchema;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("AssignedEngineers", Schema = "Maintenance")]
    public class AssignedEngineer : BaseEntity
    {

        public AssignedEngineer()
        {

         
        }

        public int Id { get; set; }
        public int WorkOrderID { get; set; }
  
        public int AssignedEngineersId { get; set; }
        //fix
        public int EngineerType { get; set; }

        public virtual ApplicationUser AssignedEngineers { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

  




    }




}

