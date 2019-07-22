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
        /// 功能名称
        /// </summary>
        public string OperationalName { get; set; }
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
        /// 操作状态(1：成功，2：异常)
        /// </summary>
        public int OperationalState { get; set; } = 1;
        /// <summary>
        /// 操作人信息
        /// </summary>
        [IsTable]
        public virtual UserManager UserManager { get; set; }
    }
}
