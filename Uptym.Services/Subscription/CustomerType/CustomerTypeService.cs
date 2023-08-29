using AutoMapper;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uptym.Core.Interfaces;
using Uptym.Data.DataContext;
using Uptym.Data.Enums;
using Uptym.DTO.Subscription.CustomerType;
using Uptym.DTO.Subscription.Plan;
using Uptym.Repositories.Subscription.CustomerType;
using Uptym.Repositories.Subscription.Plan;
using Uptym.Repositories.UOW;
using Uptym.Services.Generics;
using Uptym.Services.Global.General;
using Uptym.Services.Subscription.CustomerType;

namespace Uptym.Services.Subscription.Plan
{
    public class CustomerTypeService : GService<CustomerTypeDto, Data.DbModels.SubscriptionSchema.CustomerType, ICustomerTypeRepository>, ICustomerTypeService
    {
        private readonly IResponseDTO _response;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly ICustomerTypeRepository _customerTypeRepository;
        private readonly IGeneralService _generalService;
        private readonly IPaypalConfiguration _paypalConfiguration;

        public CustomerTypeService(IMapper mapper,
            IResponseDTO responseDTO,
            IUnitOfWork<AppDbContext> unitOfWork,
            ICustomerTypeRepository customerTypeRepository,
            IGeneralService generalService,
            IPaypalConfiguration paypalConfiguration) : base(customerTypeRepository, mapper)
        {
            _response = responseDTO;
            _unitOfWork = unitOfWork;
            _customerTypeRepository = customerTypeRepository;
            _generalService = generalService;
            _paypalConfiguration = paypalConfiguration;
        }

        public IResponseDTO GetAllCustomerTypes(int? pageIndex = null, int? pageSize = null)
        {
            try
            {
                IQueryable<Data.DbModels.SubscriptionSchema.CustomerType> customerTypes = _customerTypeRepository.GetAll(x => !x.IsDeleted);

                var total = customerTypes.Count();

                // Apply Pagination
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    customerTypes = customerTypes.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var customerTypeList = _mapper.Map<List<CustomerTypeDto>>(customerTypes.ToList());

                _response.Data = new
                {
                    List = customerTypeList,
                    Page = pageIndex ?? 0,
                    pageSize = pageSize ?? 0,
                    Total = total,
                    Pages = pageSize.HasValue && pageSize.Value > 0 ? total / pageSize : 1
                };


                _response.Message = "Ok";
                _response.IsPassed = true;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        public IResponseDTO GetAllCustomerTypesAsDrp()
        {
            //This Function is used for user's page
            try
            {
                // get users with roles
                IQueryable<Data.DbModels.SubscriptionSchema.CustomerType> customerTypes = _customerTypeRepository.GetAll(x => !x.IsDeleted);

                var customerTypesList = _mapper.Map<List<CustomerTypeDto>>(customerTypes.ToList());
                _response.Data = customerTypesList;
                _response.Message = "Ok";
                _response.IsPassed = true;
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        public async Task<IResponseDTO> CreateCustomerType(CustomerTypeDto customerTypeDto)
        {
            try
            {
                var customerType = _mapper.Map<Data.DbModels.SubscriptionSchema.CustomerType>(customerTypeDto);
                await _customerTypeRepository.AddAsync(customerType);

                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to create the CustomerType";
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "CustomerType created Successfully";
                _response.Data = _mapper.Map<CustomerTypeDto>(customerType);
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        public async Task<IResponseDTO> UpdateCustomerType(CustomerTypeDto customerTypeDto)
        {
            try
            {
                var customerType = await _customerTypeRepository.GetFirstOrDefaultAsync(x => x.Id == customerTypeDto.Id);
                customerType.Name = customerTypeDto.Name;

                _customerTypeRepository.Update(customerType);
                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to update the CustomerType";
                    return _response;
                }

                // Res
                var updateResult = _mapper.Map<CustomerTypeDto>(customerTypeDto);
                _response.IsPassed = true;
                _response.Message = "CustomerType is updated successfully";
                _response.Data = updateResult;

            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }
        public async Task<IResponseDTO> DeleteCustomerType(int customerTypeId)
        {
            try
            {
                var customerType = await _customerTypeRepository.GetFirstOrDefaultAsync(x => x.Id == customerTypeId);
                customerType.IsDeleted = true;
                customerType.UpdatedOn = DateTime.Now;

                _customerTypeRepository.Update(customerType);
                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to delete the CustomerType";
                    return _response;
                }

                // Res
                _response.IsPassed = true;
                _response.Message = "CustomerType is deleted successfully";

            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }


    }



}
