
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.CMSSchema
{
    [Table("FrequentlyAskedQuestions", Schema = "CMS")]
    public class FrequentlyAskedQuestion : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
