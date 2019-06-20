using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Repository
{
    public class ButtonRepository : EntityFrameworkRepository<Button>, IButtonRepository
    {
        public ButtonRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
