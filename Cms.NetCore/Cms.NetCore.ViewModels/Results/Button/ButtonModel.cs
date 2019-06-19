using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.Results.Button
{
    public class ButtonModel: BaseResult
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public string Description { get; set; }
    }
}
