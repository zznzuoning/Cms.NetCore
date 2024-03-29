﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.NetCore.Infrastructure.Comm;
using Cms.NetCore.Infrastructure.CusttomerAttribute;
using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using Cms.NetCore.Infrastructure.Specifications;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Button;
using Cms.NetCore.ViewModels.Results.Button;
using Microsoft.AspNetCore.Mvc;

namespace Cms.NetCore.Admin.Controllers
{
    public class ButtonController : BaseController
    {
        private readonly IButtionServices _buttionServices;
        public ButtonController(IButtionServices buttionServices, IUserManagerServices userManagerServices) : base(userManagerServices)
        {
            _buttionServices = buttionServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有按钮数据
        /// </summary>
        /// <param name="buttonSerach">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> GetList([FromQuery] ButtonSerach buttonSerach)
        {
            if (!string.IsNullOrWhiteSpace(buttonSerach.Name))
            {
                LinqComm<Button>.And(d => d.Name.Contains(buttonSerach.Name));
            }
            var result = await _buttionServices.GetListPagedAsync(Specification<Button>.Eval(LinqComm<Button>.GetExpression()), d => d.Sort, SortOrder.Ascending, buttonSerach.Page, buttonSerach.Limit);
            var data = result.data.Select(d => new ButtonList
            {
                Id = d.Id,
                Sid = d.Sid,
                Name = d.Name,
                Code = d.Code,
                Sort = d.Sort,
                UpdateTime = d.UpdateTime,
                UpdateUser = d.UpdateUser == null ? "" : d.CreateUser.RealName,
                Description = d.Description
            }).ToList();
            return Json(new PageResult<ButtonList> { code = result.code, msg = result.msg, data = data, count = result.count });
        }

        /// <summary>
        /// 根据id获取按钮信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetButtonById([FromQuery]string id)
        {
            var result = new DataResult<ButtonModel>();
            Guid gid;
            if (!Guid.TryParse(id, out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var getResult = await _buttionServices.GetAsync(gid);

            if (getResult.data != null)
            {
                var button = new ButtonModel();
                button.Id = getResult.data.Id;
                button.Name = getResult.data.Name;
                button.Code = getResult.data.Code;
                button.Icon = getResult.data.Icon;
                button.Sort = getResult.data.Sort;
                button.Description = getResult.data.Description;
                result.data = button;
                result.code = getResult.code;
                result.msg = getResult.msg;
            }
            return Json(result);
        }
        [Operation(IgnoreLog =true)]
        public IActionResult CreateOrUpdate()
        {
            return View();
        }
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="buttonAddOrUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        [Operation(OperationName ="添加或修改按钮")]
        public async Task<IActionResult> CreateOrUpdate([FromForm] ButtonAddOrUpdate buttonAddOrUpdate)
        {
            var result = new Result();
            Guid gid = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(buttonAddOrUpdate.Id))
            {
                if (!Guid.TryParse(buttonAddOrUpdate.Id, out gid))
                {
                    result.code = (int)StatusCodeEnum.HttpMehtodError;
                    result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                    return Json(result);
                }
            }
            var getResult = await _buttionServices.GetAsync(gid);
            var button = getResult.data ?? new Button();
            button.Name = buttonAddOrUpdate.Name;
            button.Code = buttonAddOrUpdate.Code;
            button.Icon = buttonAddOrUpdate.Icon;
            button.Sort = buttonAddOrUpdate.Sort;
            button.Description = buttonAddOrUpdate.Description;
            if (button.Id == Guid.Empty)
            {
                button.CreateUserId = UserManager.Id;
                var insertResult = await _buttionServices.InsertAsync(button);
                if (insertResult.code != 0)
                {
                    return Json(insertResult);
                }
            }
            else
            {
                button.UpdateUserId = UserManager.Id;
                button.UpdateTime = DateTime.Now;
                var updateResult = await _buttionServices.UpdateAsync(button);
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
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Operation(OperationName = "删除按钮")]
        public async Task<IActionResult> Delete([FromForm]string id)
        {
            var result = new Result
            {
                code = (int)StatusCodeEnum.Success,
                msg = StatusCodeEnum.Success.GetEnumText()
            };
            Guid gid;
            if (!Guid.TryParse(id, out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var getResult = await _buttionServices.GetAsync(gid);
            if (getResult.code != 0)
            {
                return Json(getResult);
            }
            var button = getResult.data;
            button.IsDelete = true;
            button.UpdateUserId = UserManager.Id;
            button.UpdateTime = DateTime.Now;
            var updateResult = await _buttionServices.UpdateAsync(button);
            if (updateResult.code != 0)
            {
                return Json(updateResult);
            }
            return Json(result);
        }
    }
}