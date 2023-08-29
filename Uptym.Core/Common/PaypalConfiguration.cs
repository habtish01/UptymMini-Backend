using Uptym.Core.Interfaces;
namespace Uptym.Core.Common
{
    public class PaypalConfiguration : IPaypalConfiguration
    {
        
        public string ProductURL { get; set; }
        public string PlanURL { get; set; }
        public string ProductID { get; set; }
        public string ClientID { get; set; }
        public string SecretKey { get; set; }
    }
}
