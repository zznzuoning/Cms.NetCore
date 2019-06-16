using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cms.NetCore.IServices;
using Cms.NetCore.ViewModels.UserManager;
using Cms.NetCore.Infrastructure.Specifications;
using Cms.NetCore.Models;
using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.ViewModels.Results.UserManager;
using Cms.NetCore.ViewModels;
using Cms.NetCore.Infrastructure.Comm;
using Cms.NetCore.Infrastructure.Extension;
using Cms.NetCore.ViewModels.param.UserManager;
using Microsoft.AspNetCore.Http;

namespace Cms.NetCore.Admin.Controllers
{
    public class UserManagerController : BaseController
    {
        private readonly IUserManagerServices _userManagerServices;
     
        public UserManagerController(IUserManagerServices userManagerServices) 
        {
            _userManagerServices = userManagerServices;
           
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有用户数据
        /// </summary>
        /// <param name="userManagerSerach">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> GetList([FromQuery] UserManagerSerach userManagerSerach)
        {
            if (!string.IsNullOrWhiteSpace(userManagerSerach.Name))
            {
                LinqComm<UserManager>.And(d => d.RealName.Contains(userManagerSerach.Name));
            }
            if (!string.IsNullOrWhiteSpace(userManagerSerach.IsEnabled))
            {
                bool isEnabled = bool.Parse(userManagerSerach.IsEnabled);
                LinqComm<UserManager>.And(d => d.IsEnabled == isEnabled);
            }
            var result = await _userManagerServices.GetListPagedAsync(Specification<UserManager>.Eval(LinqComm<UserManager>.GetExpression()), d => d.Sid, SortOrder.Descending, userManagerSerach.Page, userManagerSerach.Limit);
            var data = result.data.Select(d => new UserList
            {
                Id = d.Id,
                Sid = d.Sid,
                UserName = d.RealName,
                Email = d.Email,
                Mobilephone = d.Mobilephone,
                IsEnabled = d.IsEnabled,
                CreateUser = d.CreateUser == null ? "" :d.CreateUser.RealName,
                CreateTime = d.CreateTime
            }).ToList();
            return Json(new PageResult<UserList> { code = result.code, msg = result.msg, data = data, count = result.count });
        }
        public async Task<IActionResult> GetUserById([FromQuery]string id)
        {
            var result = new DataResult<UserModel>();
            Guid gid;
            if(!Guid.TryParse(id,out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return  Json(result);
            }
            var getResult = await _userManagerServices.GetAsync(gid);
          
            if (getResult.data != null)
            {
                var user = new UserModel();

                user.Id = getResult.data.Id;
                user.UserName = getResult.data.UserLogin.UserName;
                user.RealName = getResult.data.RealName;
                user.Remarks = getResult.data.Remarks;
                user.Mobilephone = getResult.data.Mobilephone;
                user.Email = getResult.data.Email;
                
                result.data = user;
                result.code = getResult.code;
                result.msg = getResult.msg;
            }
            return Json(result);
        }
        public IActionResult CreateOrUpdate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromForm] UserAddOrUpdate userAddOrUpdate )
        {
            var result = new Result();
            Guid gid=Guid.Empty;
            if (!string.IsNullOrWhiteSpace(userAddOrUpdate.Id))
            {
                if (Guid.TryParse(userAddOrUpdate.Id, out gid))
                {
                    result.code = (int)StatusCodeEnum.HttpMehtodError;
                    result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                    return Json(result);
                }
            }
            var getResult = await _userManagerServices.GetAsync(gid);
            var user = getResult.data ?? new UserManager();
            user.RealName = userAddOrUpdate.RealName;
            user.Remarks = userAddOrUpdate.Remarks;
            user.Mobilephone = userAddOrUpdate.Mobilephone;
            user.Email = userAddOrUpdate.Email;
            if (user.Id == Guid.Empty)
            {
                var userLogin = new UserLogin
                {
                    Id = Guid.NewGuid(),
                    UserName = userAddOrUpdate.UserName,
                    PassWord = "q123456",
                    LastLoginIp = Ip,
                    LastLoginTime = DateTime.Now,
                    LogInCount = 1
                };
                user.UserLoginId = userLogin.Id;
                user.UserLogin = userLogin;
               var insertResult = await _userManagerServices.InsertAsync(user);
                if (insertResult.code != 0)
                {
                    return Json(insertResult);
                }
            }
            else
            {
               
                user.UserLogin.UserName = userAddOrUpdate.UserName;
                var updateResult = await _userManagerServices.UpdateAsync(user);
                if (updateResult.code != 0)
                {
                    return Json(updateResult);
                }
            }
            result.code = (int)StatusCodeEnum.Success;
            result.msg = StatusCodeEnum.Success.GetEnumText();
            return Json(result);
        }
    }
}