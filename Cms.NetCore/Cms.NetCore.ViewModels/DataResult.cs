using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels
{
    /// <summary>
    /// 带数据返回的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataResult<T> : Result
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }
}
