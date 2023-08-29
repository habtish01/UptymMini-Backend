using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("TaskListDetails", Schema = "Metadata")]
    public class TaskListDetail : BaseEntity
    {
        public string Task { get; set; }
        public int TaskListId { get; set; }

        public virtual TaskList TaskList { get; set; }
    }
}
