using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    
    /// <summary>
    /// 部门表
    /// </summary>
    public class Department : BaseModel
    {
        /// <summary>
        /// 父级id
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 父级部门
        /// </summary>
        public Department Departments { get; set; }
        public ICollection<Department> Departmentss { get; set; }
        public ICollection<UserDepartment> UserDepartments { get; set; }
    }
}
