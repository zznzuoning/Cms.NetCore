using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Account;
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
        DataResult<UserManager> SignIn(LoginPara loginPara);
        Task<DataResult<UserManager>> SignInAsync(LoginPara loginPara);
        Result UpdatePassWord(UpdatePwdPara updatePwdPara);
        Task<Result> UpdatePassWordAsync(UpdatePwdPara updatePwdPara);
    }
}
