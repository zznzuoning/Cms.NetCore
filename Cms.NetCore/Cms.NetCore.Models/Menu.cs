using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    
    /// <summary>
    /// 菜单表
    /// </summary>
    public class Menu : BaseModel
    {
        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon  { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string BaseUrl { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 父类菜单
        /// </summary>
        public virtual Menu ParentMenu { get; set; }
        public virtual ICollection<Menu> ChildrenMenus { get; set; }
        public virtual ICollection<MenuButton> MenuButtons { get; set; }
    }
}
