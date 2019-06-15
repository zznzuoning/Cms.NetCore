using System;
using System.Collections.Generic;
using System.Text;
using Cms.NetCore.IRepository;
using Cms.NetCore.Models;

namespace Cms.NetCore.Repository
{
    public class UserLoginRepository : EntityFrameworkRepository<UserLogin>, IUserLoginRepository
    {
        public UserLoginRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
