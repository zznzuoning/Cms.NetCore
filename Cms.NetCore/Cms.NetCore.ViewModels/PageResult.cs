using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels
{
    /// <summary>
    /// 分页数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageResult<T> : ListResult<T>
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int count { get; set; }
    }
}
