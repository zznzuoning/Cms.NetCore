using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.param.Role
{
    public class RoleMenuPara
    {
        public Guid RoleId { get; set; }
        public List<MenuButtonAttributes> MenuButtonIds { get; set; }
    }
}
