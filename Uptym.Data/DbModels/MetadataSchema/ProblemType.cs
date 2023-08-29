using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;


#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("ProblemTypes", Schema = "Metadata")]
    public class ProblemType : DynamicLookup
    {
        public ProblemType()
        {
         }
        public string Description { get; set; }

        public int EquipmentLookupId { get; set; }
        public virtual EquipmentLookup EquipmentLookup { get; set; }
        public virtual ICollection<SubProblemType> TaskList { get; set; }
    }
}
