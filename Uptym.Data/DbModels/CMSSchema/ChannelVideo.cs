using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.CMSSchema
{
    [Table("ChannelVideos", Schema = "CMS")]
    public class ChannelVideo : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AttachmentUrl { get; set; }
        public float? AttachmentSize { get; set; }
        public string AttachmentName { get; set; }
        public string ExtensionFormat { get; set; }
        public bool IsExternalResource { get; set; } // false if the user broese and upload the resource itself
    }
}
