using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.Results.Button
{
    public class ButtonList : BaseResult
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Sort { get; set; }
        public string Description { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
