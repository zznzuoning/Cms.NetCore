using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Repository
{
    public class OperationalLogRepository : EntityFrameworkRepository<OperationalLog>, IOperationalLogRepository
    {
        public OperationalLogRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }
    }
}
