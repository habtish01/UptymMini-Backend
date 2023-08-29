using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;


#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("SubProblemTypes", Schema = "Metadata")]
    public class SubProblemType : DynamicLookup
    {
        public string Description { get; set; }
        public int ProblemTypeId { get; set; }
        public virtual ProblemType ProblemType { get; set; }
       
    }
}
