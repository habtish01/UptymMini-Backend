﻿﻿using System;
using Uptym.Core.Interfaces;

namespace Uptym.DTO.Common
{
    public class BaseEntityDto : IBaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }


        // UI
        public string Creator { get; set; }
        public string Updator { get; set; }
    }
}
