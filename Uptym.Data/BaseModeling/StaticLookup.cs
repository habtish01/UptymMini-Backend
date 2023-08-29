using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Core.Interfaces;

namespace Uptym.Data.BaseModeling
{
    public class StaticLookup : ILookupEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }
}

