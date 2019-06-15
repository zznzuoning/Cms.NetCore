using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Repository
{
    public class ButtonRepository : EntityFrameworkRepository<Buttion>, IButtonRepository
    {
        public ButtonRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
