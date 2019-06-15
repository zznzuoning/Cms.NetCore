using Cms.NetCore.IRepository;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Services
{
    public class ButtionServices : ApplicationServices<Buttion>, IButtionServices
    {
        public ButtionServices(IBaseRepository<Buttion> baseRepository) : base(baseRepository)
        {
        }
    }
}
