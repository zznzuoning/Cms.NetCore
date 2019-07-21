using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.param.UserManager
{
    public class UpdatePwdPara
    {
        public Guid Id { get; set; }
        public string NewPwd { get; set; }
        public string ConfirmPwd { get; set; }
    }
}
