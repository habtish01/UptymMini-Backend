using System;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Core.Interfaces;
using Uptym.Data.DbModels.SubscriptionSchema;

namespace Uptym.Data.BaseModeling
{
    public class CustomerEntity: IBaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("CustomerCreator")]
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("CustomerUpdator")]
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Customer CustomerCreator { get; set; }
        public virtual Customer CustomerUpdator { get; set; }

    }
}
