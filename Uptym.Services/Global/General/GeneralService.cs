using System.Collections.Generic;
using System.Linq;
using Uptym.Data.Enums;
using Uptym.Repositories.Security.UserRole;

namespace Uptym.Services.Global.General
{
    public class GeneralService : IGeneralService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        public GeneralService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public List<int> SuperAdminIds()
        {
            var userIds = _userRoleRepository.GetAll(x => x.RoleId == (int)ApplicationRolesEnum.SuperAdmin).Select(x => x.UserId).ToList();
            return userIds;
        }
    }
}
