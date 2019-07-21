using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Infrastructure.CusttomerAttribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Method, AllowMultiple = false)]
    public class OperationAttribute : Attribute
    {
        /// <summary>
        /// 忽略Log日志记录，默认false。
        /// </summary>
        public bool IgnoreLog { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string OperationName { get; set; }
    }
}
