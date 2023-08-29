
using System.Threading.Tasks;
using Uptym.Core.Interfaces;
using Uptym.DTO.Subscription.CustomerType;
using Uptym.Repositories.Subscription.CustomerType;
using Uptym.Services.Generics;

namespace Uptym.Services.Subscription.CustomerType
{
    public interface ICustomerTypeService : IGService<CustomerTypeDto, Data.DbModels.SubscriptionSchema.CustomerType, ICustomerTypeRepository>
    {
        IResponseDTO GetAllCustomerTypes(int? pageIndex = null, int? pageSize = null);
        IResponseDTO GetAllCustomerTypesAsDrp();
        Task<IResponseDTO> CreateCustomerType(CustomerTypeDto customerType);
        Task<IResponseDTO> UpdateCustomerType(CustomerTypeDto customerType);
        Task<IResponseDTO> DeleteCustomerType(int customerTypeId);

    }
}
