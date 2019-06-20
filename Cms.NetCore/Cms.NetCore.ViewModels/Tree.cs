using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels
{
    public class Tree
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Open { get; set; }
        public bool @Checked { get; set; }
        public List<Tree> Children { get; set; }
    }
}
