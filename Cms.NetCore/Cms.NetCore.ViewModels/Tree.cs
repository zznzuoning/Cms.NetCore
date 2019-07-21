using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels
{
    public class Tree:BaseTree
    {
        /// 子节点
        /// </summary>
        public List<MenuTree> Children { get; set; }
    }
    public class MenuTree: BaseTree
    {
        /// 子节点
        /// </summary>
        public List<ButtonTree> Children { get; set; }
    }
    public class ButtonTree : BaseTree
    {
        /// <summary>
        /// 节点是否初始为选中状态
        /// </summary>
        public bool Checked { get; set; }
    }
    public class BaseTree
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 节点标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 节点是否初始展开
        /// </summary>
        public bool Spread { get; set; }
        /// <summary>
        /// 自定义属性
        /// </summary>
        public MenuButtonAttributes Attributes { get; set; }
    }
    public class MenuButtonAttributes
    {
        public Guid? MenuButtonId { get; set; }
    }

    public class UserMenuTree
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 节点标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 节点是否初始展开
        /// </summary>
        public bool Spread { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        public string Href { get; set; }
        public List<UserMenuTree> Children { get; set; }
    }
}
