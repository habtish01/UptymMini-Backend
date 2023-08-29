
using Uptym.DTO.Common;

namespace Uptym.DTO.Subscription.Customer
{
    public class CustomerFilterDto : BaseFilterDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public int PlanId { get; set; }
        public string Status { get; set; }
        public string IsTrial { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerTypeId { get; set; }
    }
}
