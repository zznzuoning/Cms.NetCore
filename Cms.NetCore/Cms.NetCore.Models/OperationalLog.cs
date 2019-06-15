using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    /// <summary>
    /// 操作日志表
    /// </summary>
    public class OperationalLog : IEntity
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 操作页面
        /// </summary>
        public string Controller { get; set; }
        /// <summary>
        /// 操作的按钮
        /// </summary>
        public string Action { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid UserManagerId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationalTime { get; set; }
        /// <summary>
        /// 操作Ip
        /// </summary>
        public string OperationalIp { get; set; }
        /// <summary>
        /// 操作人信息
        /// </summary>
        public UserManager UserManager { get; set; }
    }
}
