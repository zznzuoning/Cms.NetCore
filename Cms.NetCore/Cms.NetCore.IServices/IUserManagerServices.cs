using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.UserManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.IServices
{
    public interface IUserManagerServices : IApplicationServices<UserManager>
    {
        Result SetRole(SetRolePara  setRolePara);
        Task<Result> SetRoleAsync(SetRolePara setRolePara);
    }
}
