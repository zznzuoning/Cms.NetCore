using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.param.Role
{
    public class RoleAddOrUpdate
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public bool IsDefault { get; set; }
        public string Remarks { get; set; }
    }
}
