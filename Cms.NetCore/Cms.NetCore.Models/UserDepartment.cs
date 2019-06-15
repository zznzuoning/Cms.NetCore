using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    [NotMapped]
    /// <summary>
    /// 用户部门关联表
    /// </summary>
    public class UserDepartment : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserManagerId { get; set; }
        public Guid DepartmentId { get; set; }
        public UserManager UserManager { get; set; }
        public Department Department { get; set; }
    }
}
