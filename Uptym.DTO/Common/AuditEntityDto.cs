using System;

namespace Uptym.DTO.Common
{
    public class AuditEntityDto
    {
        public int Id { get; set; }
        public DateTime DateOfAction { get; set; }
        public string Action { get; set; }
        public int CreatedBy { get; set; }

        // UI
        public string Creator { get; set; }
    }
}
