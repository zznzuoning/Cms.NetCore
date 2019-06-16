using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    /// <summary>
    /// 按钮表
    /// </summary>
    public class Buttion : BaseModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标识(js方法名称)
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public virtual ICollection<MenuButton> MenuButtons { get; set; }
    }
}
