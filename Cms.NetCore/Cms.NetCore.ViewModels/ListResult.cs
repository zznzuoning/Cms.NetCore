using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels
{
    public  class ListResult<T> :Result
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> data { get; set; }
    }
}
