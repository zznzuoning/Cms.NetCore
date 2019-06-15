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

        private readonly IRoleServices _service;
        public HomeController(IRoleServices service)
        {
           this._service = service;
        }
        public IActionResult Index()
        {
            Role role = new Role {
                Name = "超级管刘安",
                Remarks="123"
              
            };
          Result result= _service.Insert(role);
            return View();
        }

        public IActionResult Privacy()
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
