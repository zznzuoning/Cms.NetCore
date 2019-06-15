using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Repository
{
    public class RoleMenuButtonRepository : EntityFrameworkRepository<RoleMenuButton>, IRoleMenuButtonRepository
    {
        public RoleMenuButtonRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
