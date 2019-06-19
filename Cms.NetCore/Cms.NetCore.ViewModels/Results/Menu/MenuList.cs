using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.Results.Menu
{
    public class MenuList : BaseResult
    {
        public string MenuName { get; set; }
        public string ParentMenuName { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
