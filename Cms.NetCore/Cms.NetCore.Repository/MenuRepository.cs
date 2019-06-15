using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Repository
{
    public class MenuRepository : EntityFrameworkRepository<Menu>,IMenuRepository
    {
        public MenuRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
