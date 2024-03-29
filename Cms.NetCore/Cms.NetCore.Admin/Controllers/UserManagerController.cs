﻿using System;
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
using Cms.NetCore.ViewModels.Results.Home;
using Cms.NetCore.Infrastructure.CusttomerAttribute;

namespace Cms.NetCore.Admin.Controllers
{
    public class UserManagerController : BaseController
    {
        private readonly IUserManagerServices _userManagerServices;
        private readonly IRoleServices _roleServices;
        public UserManagerController(IUserManagerServices userManagerServices,IRoleServices roleServices):base(userManagerServices)
        {
            _userManagerServices = userManagerServices;
            _roleServices = roleServices;

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
                CreateUser = d.CreateUser == null ? "" : d.CreateUser.RealName,
                CreateTime = d.CreateTime
            }).ToList();
            return Json(new PageResult<UserList> { code = result.code, msg = result.msg, data = data, count = result.count });
        }
        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetUserById([FromQuery]string id)
        {
            var result = new DataResult<UserModel>();
            Guid gid;
            if (!Guid.TryParse(id, out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
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
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <returns></returns>
        [Operation(IgnoreLog = true)]
        public IActionResult CreateOrUpdate()
        {
            return View();
        }
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="userAddOrUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        [Operation(OperationName = "添加或修改用户")]
        public async Task<IActionResult> CreateOrUpdate([FromForm] UserAddOrUpdate userAddOrUpdate)
        {
            var result = new Result();
            Guid gid = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(userAddOrUpdate.Id))
            {
                if (!Guid.TryParse(userAddOrUpdate.Id, out gid))
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
                user.CreateUserId = UserManager.Id;
                var userLogin = new UserLogin
                {
                    Id = Guid.NewGuid(),
                    UserName = userAddOrUpdate.UserName,
                    PassWord = Md5.GetMD5String("q123456"),
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
                user.UpdateUserId = UserManager.Id;
                user.UpdateTime =DateTime.Now;
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

        /// <summary>
        /// 启用或禁用或删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Operation(OperationName = "删除或禁用用户")]
        public async Task<IActionResult> SetIsEnableOrDelete([FromForm]SetIsEnableOrDelete setIsEnableOrDelete)
        {
            var result = new Result
            {
                code = (int)StatusCodeEnum.Success,
                msg = StatusCodeEnum.Success.GetEnumText()
            };
            if (string.IsNullOrEmpty(setIsEnableOrDelete.Id))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            Guid gid;
            if (!Guid.TryParse(setIsEnableOrDelete.Id, out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var getResult = await _userManagerServices.GetAsync(gid);
            if (getResult.code != 0)
            {
                return Json(getResult);
            }
            var user = getResult.data;
            if (setIsEnableOrDelete.IsDelete)
            {
                user.IsDelete = true;
            }
            else
            {
                user.IsEnabled = !user.IsEnabled;
            }
            user.UpdateUserId = UserManager.Id;
            user.UpdateTime = DateTime.Now;
            var updateResult = await _userManagerServices.UpdateAsync(user);
            if (updateResult.code != 0)
            {
                return Json(updateResult);
            }
            return Json(result);
        }
        [Operation(IgnoreLog = true)]
        public IActionResult SetRole()
        {
            return View();
        }
        [HttpPost]
        [Operation(OperationName = "设置角色")]
        public async Task<IActionResult> SetRole([FromForm]SetRolePara setRolePara)
        {

            var result = new Result
            {
                code = (int)StatusCodeEnum.Success,
                msg = StatusCodeEnum.Success.GetEnumText()
            };
            Guid gid = Guid.Empty;
            if (!Guid.TryParse(setRolePara.Id, out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(setRolePara.RoleIds))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var setResult = await _userManagerServices.SetRoleAsync(setRolePara);
            if (setResult.code != 0)
            {
                return Json(setResult);
            }
            return Json(result);
        }
        /// <summary>
        /// 获取所有按钮
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetRole(Guid id)
        {
            var getResult = await _roleServices.GetListAsync(Specification<Role>.Eval(d => !d.IsDelete));
            var roles = getResult.data.Select(d => new Select
            {
                value = d.Id.ToString(),
                Name = d.Name,
                selected = d.UserRoles.Any(x => x.RoleId == d.Id && x.UserManagerId == id)
            }).ToList();
            return Json(roles);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [Operation(IgnoreLog = true)]
        public IActionResult UpdatePassWord()
        {
            var user = new UserInfo
            {
                UserName = UserManager.RealName
            };
            return View(user);
        }
        [HttpPost]
        [Operation(OperationName = "修改密码")]
        public async Task<IActionResult> UpdatePassWord([FromForm]UpdatePwdPara updatePwdPara)
        {
            var result = new Result {
                code = (int)StatusCodeEnum.Success,
                msg = StatusCodeEnum.Success.GetEnumText()
            };
            if (string.IsNullOrWhiteSpace(updatePwdPara.NewPwd) || string.IsNullOrWhiteSpace(updatePwdPara.ConfirmPwd))
            {
                result.code = (int)StatusCodeEnum.HttpRequestError;
                result.msg = StatusCodeEnum.HttpRequestError.GetEnumText();
                return Json(result);
            }
            if (!updatePwdPara.ConfirmPwd.Equals(updatePwdPara.NewPwd, StringComparison.OrdinalIgnoreCase))
            {
                result.code = (int)StatusCodeEnum.ConfirmPwdError;
                result.msg = StatusCodeEnum.ConfirmPwdError.GetEnumText();
                return Json(result);
            }
            string newPwd = Md5.GetMD5String(updatePwdPara.NewPwd);
            if (UserManager.UserLogin.PassWord.Equals(newPwd, StringComparison.OrdinalIgnoreCase))
            {
                result.code = (int)StatusCodeEnum.NewPasswordEqualOldError;
                result.msg = StatusCodeEnum.NewPasswordEqualOldError.GetEnumText();
                return Json(result);
            }
            updatePwdPara.Id = UserManager.UserLoginId;
            updatePwdPara.NewPwd = newPwd;
            var updateResult =await _userManagerServices.UpdatePassWordAsync(updatePwdPara);
            if (updateResult.code != 0)
            {
                return Json(updateResult);
            }
            return Json(result);
        }
        /// <summary>
        /// 个人资料
        /// </summary>
        /// <returns></returns>
        public IActionResult UserManagerInfo()
        {
            return View();
        }
    }
}