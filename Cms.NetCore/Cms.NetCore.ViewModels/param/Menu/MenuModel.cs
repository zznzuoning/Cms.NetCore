using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.param.Menu
{
   public  class MenuAddOrUpdate
    {
        public string Id { get; set; }
        public string MenuName { get; set; }
        public string Code { get; set; }
        public string ParentId { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
    }
}
