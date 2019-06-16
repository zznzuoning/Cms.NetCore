using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
 
    /// <summary>
    /// 角色菜单按钮关联表
    /// </summary>
    public class RoleMenuButton : IEntity
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid MenuButtonId { get; set; }
        public virtual Role Role { get; set; }
        public virtual MenuButton MenuButton { get; set; }
       
    }
}
