using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
   
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class UserManager : BaseModel
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobilephone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 登录信息Id
        /// </summary>
        public Guid UserLoginId { get; set; }
      
        /// <summary>
        /// 用户登录信息
        /// </summary>
        public virtual UserLogin UserLogin { get; set; }

        public virtual ICollection<OperationalLog> OperationalLogs { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserDepartment> UserDepartments { get; set; }
        //public ICollection<Department> Departments { get; set; }
    }
}
