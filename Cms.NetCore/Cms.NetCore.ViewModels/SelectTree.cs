using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels
{
    public class SelectTree
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Open { get; set; }
        public bool @Checked { get; set; }
        public List<SelectTree> Children { get; set; }
    }
}
