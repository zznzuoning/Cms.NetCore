using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Infrastructure.enums
{
    public enum SortOrder
    {
        /// <summary>
        ///不排序
        /// </summary>
        Unspecified = -1,
        /// <summary>
        /// 正序
        /// </summary>
        Ascending = 0,
        /// <summary>
        /// 倒序
        /// </summary>
        Descending = 1
    }
}
