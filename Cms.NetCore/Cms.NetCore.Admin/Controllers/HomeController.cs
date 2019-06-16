using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.NetCore.Admin.Models;
using Cms.NetCore.Models;
using Cms.NetCore.Models.ModelConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cms.NetCore.IServices;
using Cms.NetCore.ViewModels;

namespace Cms.NetCore.Admin.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserManagerServices _service;
        public HomeController(IUserManagerServices service)
        {
           this._service = service;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Main()
        {

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
