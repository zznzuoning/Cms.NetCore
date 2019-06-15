using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Infrastructure.CusttomerAttribute
{
    /// <summary>
    /// 状态码text属性
    /// </summary>
    public class TextAttribute : Attribute
    {
        public string Value { get; set; }
        public TextAttribute(string Value)
        {
            this.Value = Value;
        }

       
    }
}
