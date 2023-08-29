
using Uptym.DTO.Common;

namespace Uptym.DTO.Security.User
{
    public class UserFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Status { get; set; }
        public string PhoneNumber { get; set; }
        public int UserSubscriptionLevelId { get; set; }
        public int? CustomerId { get; set; }

        public int? CustomerTypeId { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? HealthFacilityTypeId { get; set; }
    }
}
