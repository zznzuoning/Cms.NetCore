using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels
{
    public class DistinctMenu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
