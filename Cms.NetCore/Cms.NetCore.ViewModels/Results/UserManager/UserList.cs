using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.Results.UserManager
{
    public class UserList: BaseResult
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobilephone { get; set; }
        public bool IsEnabled { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
