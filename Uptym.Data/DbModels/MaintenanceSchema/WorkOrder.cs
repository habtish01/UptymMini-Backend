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
    [Table("WorkOrders", Schema = "Maintenance")]
    public class WorkOrder : BaseEntity
    {

        public WorkOrder()
        {

            AssignedEngineers = new HashSet<AssignedEngineer>();
            MaintenanceActions = new HashSet<MaintenanceAction>();
            WorkOrderStatus = new HashSet<WorkOrderStatus>();
        }

        public string Title { get; set; }
        public string PlannedStartDate { get; set; }
        public string PlannedEndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public string? OfflineContractor  { get; set; }
        public int? MaintenanceHeadId { get; set; }
        public int? ForwardedFromId { get; set; }
        public int WorkOrderHeaderId { get; set; }
        public int? ContractorId { get; set; }
        public int? AssignedEngineerId { get; set; }
        public int? ManagerPriorityId { get; set; }
        public virtual ApplicationUser AssignedEngineer { get; set; }
      
        public virtual Customer ForwardedFrom { get; set; }
        public virtual Customer Contractor { get; set; }
        public virtual Priority ManagerPriority { get; set; }
        public virtual ApplicationUser MaintenanceHead { get; set; }
         public virtual WorkOrderHeader WorkOrderHeader { get; set; }
        public virtual ICollection<MaintenanceAction> MaintenanceActions { get; set; }
        public virtual ICollection<WorkOrderStatus> WorkOrderStatus { get; set; }
        public virtual ICollection<AssignedEngineer> AssignedEngineers { get; set; }
        public virtual ICollection<MaintenanceTasks> MaintenanceTasks { get; set; }

        [NotMapped]
        public DateTime PlannedStartDateCheck
        {
            get
            {
              return  Convert.ToDateTime(PlannedStartDate);
            }
        }
        public virtual string MaintenanceActionNotes
        {
            get
            {
                string notes = "";
                foreach (MaintenanceAction actions in MaintenanceActions)
                {
                    //  if(notes != "")

                    notes =   actions.Title;


                }
                return notes;
            }


        }


        public virtual DateTime WorkorderStatusesdate
        {
            get
            {
                DateTime WSD = DateTime.Today;
                foreach (WorkOrderStatus ws in WorkOrderStatus)
                {
                    WSD = ws.StatusDate;
                }
                return WSD;
            }


        }

      
    }




}

