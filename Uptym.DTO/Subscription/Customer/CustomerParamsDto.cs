using System;
using Uptym.DTO.Common;

namespace Uptym.DTO.Subscription.Customer
{
    public class ChangeCustomerStatusParamsDto
    {
        public int CustomerId { get; set; }
        public string Status { get; set; }
        public LocationDto LocationDto { get; set; }
    }

}
