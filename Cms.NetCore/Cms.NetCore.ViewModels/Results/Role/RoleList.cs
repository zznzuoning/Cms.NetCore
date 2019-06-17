using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.ViewModels.Results.Role
{
    public class RoleList : BaseResult
    {
        public string RoleName { get; set; }
        public string Remarks { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateUser { get; set; }
        public bool IsDefault { get; set; }
    }
}
