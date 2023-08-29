using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uptym.Core.Interfaces;
using Uptym.Data.DataContext;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.DTO.Security;
using Uptym.Repositories.Security.Role;
using Uptym.Repositories.UOW;
using Uptym.Services.Generics;
using Uptym.Services.Global.General;

namespace Uptym.Services.Security.Role
{
    public class RoleService : GService<RoleDto, ApplicationRole, IRoleRepository>, IRoleService
    {
        private readonly IResponseDTO _response;
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        private readonly IGeneralService _generalService;

        public RoleService(IMapper mapper,
            IResponseDTO responseDTO,
            IUnitOfWork<AppDbContext> unitOfWork,
            IRoleRepository roleRepository,
            IGeneralService generalService) : base(roleRepository, mapper)
        {
            _response = responseDTO;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _generalService = generalService;
        }

        public IResponseDTO GetAllRoles(int? pageIndex = null, int? pageSize = null)
        {
            try
            {
                IQueryable<ApplicationRole> roles = _roleRepository.GetAll(x => !x.IsDeleted);

                var total = roles.Count();

                // Apply Pagination
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    roles = roles.Skip((pageIndex.Value - 1) * pageSize.Value).Take(pageSize.Value);
                }

                var roleList = _mapper.Map<List<RoleDto>>(roles.ToList());

                _response.Data = new
                {
                    List = roleList,
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

        public IResponseDTO GetAllRolesAsDrp()
        {
            //This Function is used for user's page
            try
            {
                IQueryable<ApplicationRole> roles = _roleRepository.GetAll(x => !x.IsDeleted);

                var roleList = _mapper.Map<List<RoleDto>>(roles.ToList());
                _response.Data = roleList;
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

        public async Task<IResponseDTO> CreateRole(RoleDto roleDto)
        {
            try
            {
                var role = _mapper.Map<ApplicationRole>(roleDto);
                role.NormalizedName = roleDto.Name.ToUpper();
                await _roleRepository.AddAsync(role);

                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to create the Role";
                    return _response;
                }

                _response.IsPassed = true;
                _response.Message = "Role created Successfully";
                _response.Data = _mapper.Map<RoleDto>(role);
            }
            catch (Exception ex)
            {
                _response.Data = null;
                _response.Message = "Error " + ex.Message;
                _response.IsPassed = false;
            }

            return _response;
        }

        public async Task<IResponseDTO> UpdateRole(RoleDto roleDto)
        {
            try
            {
                var role = await _roleRepository.GetFirstOrDefaultAsync(x => x.Id == roleDto.Id);
                role.Name = roleDto.Name;
                role.RoleType = roleDto.RoleType;
                role.NormalizedName = roleDto.Name.ToUpper();

                _roleRepository.Update(role);
                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to update the Role";
                    return _response;
                }

                // Res
                var updateResult = _mapper.Map<RoleDto>(roleDto);
                _response.IsPassed = true;
                _response.Message = "Role is updated successfully";
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

        public async Task<IResponseDTO> DeleteRole(int roleId)
        {
            try
            {
                var role = await _roleRepository.GetFirstOrDefaultAsync(x => x.Id == roleId);
                role.IsDeleted = true;
                _roleRepository.Update(role);
                // Commit
                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    _response.Data = null;
                    _response.IsPassed = false;
                    _response.Message = "Faild to delete the Role";
                    return _response;
                }

                // Res
                _response.IsPassed = true;
                _response.Message = "Role is deleted successfully";

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
