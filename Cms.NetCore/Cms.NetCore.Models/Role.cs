using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role : BaseModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否默认(默认的禁止删除)
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 角色的操作权限
        /// </summary>
        public virtual ICollection<RoleMenuButton> RoleMenuButtons { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
       
    }
}
