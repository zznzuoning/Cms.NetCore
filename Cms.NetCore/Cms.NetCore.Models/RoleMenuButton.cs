using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    [NotMapped]
    /// <summary>
    /// 角色菜单按钮关联表
    /// </summary>
    public class RoleMenuButton : IEntity
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuButtonId { get; set; }
        public Role Role { get; set; }
        public MenuButton MenuButton { get; set; }
       
    }
}
