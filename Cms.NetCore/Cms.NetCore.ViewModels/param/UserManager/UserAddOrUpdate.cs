using Cms.NetCore.ViewModels.Results.UserManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.param.UserManager
{
    public class UserAddOrUpdate 
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobilephone { get; set; }

        public string RealName { get; set; }
        public string Remarks { get; set; }
    }
}
