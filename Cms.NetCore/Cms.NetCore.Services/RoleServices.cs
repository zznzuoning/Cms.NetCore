using Cms.NetCore.IRepository;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Services
{
    public class RoleServices : ApplicationServices<Role>, IRoleServices
    {
        public RoleServices(IBaseRepository<Role> baseRepository) : base(baseRepository)
        {
        }
    }
}
