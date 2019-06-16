using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.NetCore.Admin.Controllers
{
    public class BaseController : Controller
    {
     

        public string Ip { get {
                return HttpContext.Connection.RemoteIpAddress.ToString();
            } }
    }
}