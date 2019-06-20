using Cms.NetCore.IRepository;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Services
{
    public class ButtionServices : ApplicationServices<Button>, IButtionServices
    {
        public ButtionServices(IBaseRepository<Button> baseRepository) : base(baseRepository)
        {
        }
    }
}
