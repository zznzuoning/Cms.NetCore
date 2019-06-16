using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    
    /// <summary>
    /// 用户登录表
    /// </summary>
    public class UserLogin : IEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
       /// <summary>
       /// 登录次数
       /// </summary>
        public int LogInCount { get; set; }
        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual UserManager UserManager { get; set; }
    }
}
