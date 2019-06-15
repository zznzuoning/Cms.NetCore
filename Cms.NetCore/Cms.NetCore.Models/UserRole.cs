using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    [NotMapped]
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    public class UserRole : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserManagerId { get; set; }
        public Guid RoleId { get; set; }
        public UserManager UserManager { get; set; }
        public Role Role { get; set; }
    }
}
