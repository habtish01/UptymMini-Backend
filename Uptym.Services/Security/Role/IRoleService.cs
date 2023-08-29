
using System.Threading.Tasks;
using Uptym.Core.Interfaces;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.DTO.Security;
using Uptym.Repositories.Security.Role;
using Uptym.Services.Generics;

namespace Uptym.Services.Security.Role
{
    public interface IRoleService : IGService<RoleDto, Data.DbModels.SecuritySchema.ApplicationRole, IRoleRepository>
    {
        IResponseDTO GetAllRoles(int? pageIndex = null, int? pageSize = null);
        IResponseDTO GetAllRolesAsDrp();
        Task<IResponseDTO> CreateRole(RoleDto roleDto);
        Task<IResponseDTO> UpdateRole(RoleDto roleDto);
        Task<IResponseDTO> DeleteRole(int roleId);

    }
}
