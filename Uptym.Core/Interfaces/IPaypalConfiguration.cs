namespace Uptym.Core.Interfaces
{
    public interface IPaypalConfiguration
    {
        string ProductURL { get; set; }
        string PlanURL { get; set; }
        string ProductID { get; set; }
        string ClientID { get; set; }
        string SecretKey { get; set; }
    }
}
