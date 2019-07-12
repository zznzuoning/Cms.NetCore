using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.IServices
{
    public interface IRoleServices : IApplicationServices<Role>
    {
        ListResult<Tree> GetAllMenuButtonByRoleId(Guid id);
        Task<ListResult<Tree>> GetAllMenuButtonByRoleIdAsync(Guid id);
        Result Authorize(RoleMenuPara roleMenuPara);
        Task<Result> AuthorizeAsync(RoleMenuPara roleMenuPara);
    }
}
