using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("TaskListType", Schema = "Metadata")]
    public class TaskListType : StaticLookup
    {
        // Master,Customer
        public TaskListType()
        {
            TaskLists = new HashSet<TaskList>();
            ProblemTypes = new HashSet<ProblemType>();
        }
        public virtual ICollection<TaskList> TaskLists { get; set; }
        public virtual ICollection<ProblemType> ProblemTypes { get; set; }
    }
}
