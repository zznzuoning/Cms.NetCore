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

namespace Cms.NetCore.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly CmsDBContext _context;

        public HomeController(CmsDBContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            Role role = new Role {
                Name = "超级管刘安",
                Remarks="123"
              
            };
            _context.Set<Role>().Add(role);
            _context.SaveChanges();
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
