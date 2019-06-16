using Cms.NetCore.ViewModels.param;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.UserManager
{
    public class UserManagerSerach : PagePara
    {
        public string Name { get; set; }
        public string IsEnabled { get; set; }
    }
}
