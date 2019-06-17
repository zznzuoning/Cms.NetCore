using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.Results.Role
{
    public class RoleModel : BaseResult
    {
        public string RoleName { get; set; }
        public string Remarks { get; set; }
        public bool IsDefault { get; set; }
    }
}
