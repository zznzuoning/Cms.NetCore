using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.Results.Home
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string HeadImgUrl { get; set; }
        public int LoginCount { get; set; }
        public string Ip { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
