using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.param.Account
{
   public  class LoginPara
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string CaptchaCode { get; set; }
        public string Ip { get; set; }
        public string ReturnUrl { get; set; }
    }
}
