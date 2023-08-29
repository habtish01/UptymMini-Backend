using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Uptym.Core.Common;
using Uptym.Core.Interfaces;
using Uptym.DTO.Common;
using Uptym.DTO.Subscription.Customer;
using Uptym.DTO.Subscription.Membership;
using Uptym.Repositories.Subscription.Customer;
using Uptym.Services.Generics;

namespace Uptym.Services.Subscription.Customer
{
    public interface ICustomerService : IGService<CustomerDto, Data.DbModels.SubscriptionSchema.Customer, ICustomerRepository>
    {
        IResponseDTO GetAllAsDrp(CustomerFilterDto filterDto = null);
        //Task<IResponseDTO> Register(CustomerDto customers, MembershipDto membershipDto, IFormFile file);
        Task<IResponseDTO> Register(CustomerDto customers, IFormFile file);
        Task<IResponseDTO> GetCustomerDetails(int customerId);
        Task<IResponseDTO> GetCustomerDetailsByUserId(int loggedInUserId);
        Task<IResponseDTO> GetCustomerMembershipInfo(int loggedInUserId);
        IResponseDTO GetAll(int? pageIndex = null, int? pageSize = null, CustomerFilterDto filterDto = null);
        Task<IResponseDTO> CreateCustomer(CustomerDto customerDto, IFormFile file);
        Task<IResponseDTO> UpdateCustomer(string rootPath, CustomerDto customerDto, IFormFile file); 
        Task<IResponseDTO> UpdateCustomerReminder(int loggedInUserId, int reminderDays); 
         Task<IResponseDTO> UpdateCustomerStatus(int customerId, string status, LocationDto locationDto);
        Task<GeneratedFile> ExportUsers(int? pageIndex = null, int? pageSize = null, CustomerFilterDto filterDto = null);

    }
}
