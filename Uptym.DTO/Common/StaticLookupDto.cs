using Uptym.Core.Interfaces;

namespace Uptym.DTO.Common
{
    public class StaticLookupDto: ILookupEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }
}
