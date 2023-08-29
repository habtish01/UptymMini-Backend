using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SubscriptionSchema;

using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.EquipmentSchema
{
    [Table("Equipments", Schema = "Equipment")]
    public class Equipment : BaseEntity
    {
        public Equipment()
        {
            EquipmentContracts = new HashSet<EquipmentContract>();
            WorkOrderHeaders = new HashSet<WorkOrderHeader>();
            AutoSchedules = new HashSet<EquipmentLookupSchedule>();
            EquipmentOperators = new HashSet<EquipmentOperator>();
        }

        public string Name { get; set; }

        public string Tag { get; set; }
        public int EquipmentLookupId { get; set; }
        public int HealthFacilityId { get; set; }
        public int OwnerId { get; set; }
      
        public string SerialNumber { get; set; }
        public int EquipmentStatusId { get; set; }
        public DateTime? InstalledDate { get; set; }
        public string? InstalledBy { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime LastPreventiveMaintenanceDate { get; set; }

        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? ChasisNo { get; set; }
        // New Fields // 
        public string? Dimension { get; set; }
        public string? Weight { get; set; }
        public string? Voltage { get; set; }
        public string? Power { get; set; }
        public string? MachineCurrent { get; set; }
        public string? Frequency { get; set; }
        public string? Price { get; set; }
        public string? PurchaseDate { get; set; }
        public string? Processor { get; set; }
        public string? HardDisk { get; set; }
        public string? Ram { get; set; }
        public string? AdditionalName { get; set; }
        public string? EquipDetails { get; set; }
        public string? EngineNo { get; set; }
        public string? DCapacity { get; set; }
        public string? WCapacity { get; set; }
        public string? Units { get; set; }

        // End New Fields // 

        public int? StartKMReading { get; set; }
        public int? FuelConsumption { get; set; }

        public int? NumberOfHoursWorked { get; set; }
        public string? PlateNo { get; set; }
        public string? AssignedDriver { get; set; }
        public virtual HealthFacility HealthFacility { get; set; }
        public virtual Customer Owner { get; set; }
        public virtual EquipmentStatusEnum EquipmentStatus { get; set; }
        public virtual EquipmentLookup EquipmentLookup { get; set; }

        public virtual ICollection<EquipmentContract> EquipmentContracts { get; set; }
        public virtual ICollection<WorkOrderHeader> WorkOrderHeaders { get; set; }
        public virtual ICollection<EquipmentLookupSchedule> AutoSchedules { get; set; }
        public virtual ICollection<EquipmentOperator> EquipmentOperators { get; set; }
    }
}



