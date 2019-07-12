using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cms.NetCore.Models;
using Cms.NetCore.ViewModels.Results.MenuButton;

namespace Cms.NetCore.IRepository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        bool DelRoleMenuButtonByRoleId(Guid id);
        Task<bool> DelRoleMenuButtonByRoleIdAsync(Guid id);
        bool Authorize(List<RoleMenuButton> roleMenuButton);
        Task<bool> AuthorizeAsync(List<RoleMenuButton> roleMenuButton);
        List<RoleMenuButtonList> GetAllMenuButtonByRoleId(Guid id);
        Task<List<RoleMenuButtonList>> GetAllMenuButtonByRoleIdAsync(Guid id);
    }
}
