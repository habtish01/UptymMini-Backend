﻿using System;
using System.Collections.Generic;
using Uptym.DTO.Common;

namespace Uptym.DTO.Security
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PersonalImagePath { get; set; }
        public string IP { get; set; }
        public bool ChangePassword { get; set; }
        public string CallingCode { get; set; }
        public string Organization { get; set; }
        public int? CustomerTypeId { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Specialties { get; set; }
        public string Position { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? HealthFacilityTypeId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string Status { get; set; } // Active, NotActive, Locked
        public string ElectronicSignature { get; set; }
        public DateTime NextPasswordExpiryDate { get; set; }
        public DateTime? EmailVerifiedDate { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public int LoggedInUserCount { get; set; }
        public int? CustomerId { get; set; }
        public int? UserRoleLevelId { get; set; }
        public List<int> UserRoleLevels { get; set; }
        public List<string> UserRoles { get; set; }

        // UI

        public List<string> HealthFacilityNames { get; set; }

        public string HealthFacilityName { get; set; }
        public string HealthFacilityTypeName { get; set; }
        public string UserRoleLevelName { get; set; }
        public bool ReomveProfileImage { get; set; }
    }

    public class UserDrp
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PersonalImagePath { get; set; }
        public int? HealthFacilityId { get; set; }
    }
}
