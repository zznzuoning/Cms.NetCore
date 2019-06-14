using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cms.NetCore.Models
{
    [NotMapped]
    /// <summary>
    /// 菜单按钮关联表
    /// </summary>
    public class MenuButton
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }
        public Guid ButtonId { get; set; }
        public Menu Menu { get; set; }
        public Buttion Buttion { get; set; }
    }
}
