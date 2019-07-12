using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.Results.MenuButton
{
    public class RoleMenuButtonList
    {
        public Guid MenuId { get; set; }
        public string MenuName { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ButtonId { get; set; }
        public string ButtonName { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? MenuButtonId { get; set; }
        public bool Checked { get; set; }
    }
}
