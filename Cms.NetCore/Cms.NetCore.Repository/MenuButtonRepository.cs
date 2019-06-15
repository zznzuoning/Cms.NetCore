using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Repository
{
    public class MenuButtonRepository : EntityFrameworkRepository<MenuButton>,IMenuButtonRepository
    {
        public MenuButtonRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
