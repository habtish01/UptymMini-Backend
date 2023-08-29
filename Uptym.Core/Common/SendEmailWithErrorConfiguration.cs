using Uptym.Core.Interfaces;

namespace Uptym.Core.Common
{
    public class SendEmailWithErrorConfiguration : ISendEmailWithErrorConfiguration
    {
        public bool AllowSend { get; set; }
        public string ToEmails { get; set; }
    }
}
