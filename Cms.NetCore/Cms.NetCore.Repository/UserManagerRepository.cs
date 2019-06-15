using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Repository
{
    public class UserManagerRepository : EntityFrameworkRepository<UserManager>, IUserManagerRepository
    {
        public UserManagerRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
