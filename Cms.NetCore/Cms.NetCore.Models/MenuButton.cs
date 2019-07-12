using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
  
    /// <summary>
    /// 菜单按钮关联表
    /// </summary>
    public class MenuButton :IEntity
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public Guid ButtonId { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual Button Button { get; set; }
        public virtual RoleMenuButton RoleMenuButton { get; set; }
    }
}
