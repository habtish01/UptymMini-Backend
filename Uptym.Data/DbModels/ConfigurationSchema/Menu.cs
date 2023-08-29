using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uptym.Data.DbModels.ConfigurationSchema
{
    [Table("Menus", Schema = "Configuration")]
    public class Menu
    {
        public Menu()
        {
            MenuRoles = new HashSet<MenuRole>();
            MenuPlans = new HashSet<MenuPlan>();
        }
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string IconType { get; set; }
        public string Icon { get; set; }
        public string Class { get; set; }
        public bool GroupTitle { get; set; }
        public string Badge { get; set; }
        public string BadgeClass { get; set; }
        public int ParentMenuId { get; set; }
        public int Order { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<MenuRole> MenuRoles { get; set; }
        public virtual ICollection<MenuPlan> MenuPlans { get; set; }

    }
}
