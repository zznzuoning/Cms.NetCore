using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.NetCore.Admin.Filter;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.NetCore.Admin.Controllers
{
    [ServiceFilter(typeof(OperationalLogFilterAttribute))]
    public class BaseController : Controller
    {
        private readonly IUserManagerServices _userManagerServices;
        public BaseController(IUserManagerServices userManagerServices)
        {
            _userManagerServices = userManagerServices;
        }

        public string Ip
        {
            get
            {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }
        public UserManager UserManager
        {
            get {
                Guid id = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(d => d.Type == "id").Value);
                return _userManagerServices.Get(id).data;

            }
        }
    }
}