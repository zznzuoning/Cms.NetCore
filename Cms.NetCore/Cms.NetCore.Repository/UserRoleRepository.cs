using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Repository
{
    public class UserRoleRepository : EntityFrameworkRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
