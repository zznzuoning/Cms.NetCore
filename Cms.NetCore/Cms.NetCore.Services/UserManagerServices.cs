using Cms.NetCore.IRepository;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Services
{
    public class UserManagerServices : ApplicationServices<UserManager>, IUserManagerServices
    {
        public UserManagerServices(IBaseRepository<UserManager> baseRepository) : base(baseRepository)
        {
        }
    }
}
