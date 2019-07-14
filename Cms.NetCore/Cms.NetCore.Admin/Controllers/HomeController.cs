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
using Cms.NetCore.ViewModels.Results.Home;

namespace Cms.NetCore.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUserManagerServices userManagerServices) : base(userManagerServices)
        {

        }
        public IActionResult Index()
        {
            var user = new UserInfo
            {
                UserName = UserManager.RealName,
                HeadImgUrl = UserManager.HeadImgUrl ?? "/images/userface1.jpg"
            };
            return View(user);
        }

        public IActionResult Main()
        {

            var user = new UserInfo
            {
               LoginCount=UserManager?.UserLogin?.LogInCount??0,
               Ip= UserManager?.UserLogin?.LastLoginIp ?? "",
               LastLoginTime= UserManager?.UserLogin?.LastLoginTime??DateTime.Now
            };
            return View(user);

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
