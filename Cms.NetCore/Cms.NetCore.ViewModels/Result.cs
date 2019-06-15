using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels
{
    /// <summary>
    /// 基本操作
    /// </summary>
    public class Result
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = (int)StatusCode.Success;
        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; } = StatusCode.Success.GetEnumText();
    }
}
