using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{

    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class UserRole : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserManagerId { get; set; }
        public Guid RoleId { get; set; }
        public virtual UserManager UserManager { get; set; }
        public virtual Role Role { get; set; }
    }
}
