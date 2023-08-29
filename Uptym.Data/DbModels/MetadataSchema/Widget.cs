using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("Widgets", Schema = "Metadata")]
    public class Widget
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int WidgetTag { get; set; }
        
    }
}
