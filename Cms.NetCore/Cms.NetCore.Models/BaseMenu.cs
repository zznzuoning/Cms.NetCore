using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models
{
    public class BaseMenu: BaseModel
    {

        public Guid? ParentId { get; set; }
       
    }
}
