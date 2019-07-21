using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.NetCore.Infrastructure.Comm;
using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using Cms.NetCore.Infrastructure.Specifications;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Role;
using Cms.NetCore.ViewModels.Results.Role;
using Microsoft.AspNetCore.Mvc;

namespace Cms.NetCore.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private readonly IRoleServices _roleServices;
        public RoleController(IRoleServices roleServices,IUserManagerServices userManagerServices):base(userManagerServices)
        {
            _roleServices = roleServices;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取所有角色数据
        /// </summary>
        /// <param name="roleSerach">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> GetList([FromQuery]RoleSerach roleSerach)
        {
            if (!string.IsNullOrWhiteSpace(roleSerach.Name))
            {
                LinqComm<Role>.And(d => d.Name.Contains(roleSerach.Name));
            }
            var getResult = await _roleServices.GetListPagedAsync(Specification<Role>.Eval(LinqComm<Role>.GetExpression()), d => d.Sid, SortOrder.Descending, roleSerach.Page, roleSerach.Limit);
            var roleList = getResult.data.Select(d => new RoleList
            {
                Id = d.Id,
                RoleName = d.Name,
                Sid = d.Sid,
                Remarks = d.Remarks,
                UpdateTime = d.UpdateTime,
                UpdateUser = d.UpdateUser?.RealName,
                IsDefault=d.IsDefault
            }).ToList();

            return Json(new PageResult<RoleList>
            {
                code = getResult.code,
                msg = getResult.msg,
                count = getResult.count,
                data = roleList
            });
        }

        public IActionResult CreateOrUpdate()
        {
            return View();
        }

        /// <summary>
        /// 根据id获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetRoleById([FromQuery]string id)
        {
            var result = new DataResult<RoleModel>();
            Guid gid;
            if (!Guid.TryParse(id, out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var getResult = await _roleServices.GetAsync(gid);

            if (getResult.data != null)
            {
                var role = new RoleModel();

                role.Id = getResult.data.Id;
                role.RoleName = getResult.data.Name;
                role.Remarks = getResult.data.Remarks;
                role.IsDefault = getResult.data.IsDefault;

                result.data = role;
                result.code = getResult.code;
                result.msg = getResult.msg;
            }
            return Json(result);
        }
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="userAddOrUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromForm] RoleAddOrUpdate  roleAddOrUpdate)
        {
            var result = new Result();
            Guid gid = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(roleAddOrUpdate.Id))
            {
                if (!Guid.TryParse(roleAddOrUpdate.Id, out gid))
                {
                    result.code = (int)StatusCodeEnum.HttpMehtodError;
                    result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                    return Json(result);
                }
            }
            var getResult = await _roleServices.GetAsync(gid);
            var role = getResult.data ?? new Role();
            role.Name = roleAddOrUpdate.RoleName;
            role.Remarks = roleAddOrUpdate.Remarks;
            role.IsDefault = roleAddOrUpdate.IsDefault;
            if (role.Id == Guid.Empty)
            {
                role.CreateUserId = UserManager.Id;
                var insertResult = await _roleServices.InsertAsync(role);
                if (insertResult.code != 0)
                {
                    return Json(insertResult);
                }
            }
            else
            {
                role.UpdateUserId = UserManager.Id;
                role.UpdateTime = DateTime.Now;
                var updateResult = await _roleServices.UpdateAsync(role);
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
        public async Task<IActionResult>Delete([FromForm]string id)
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
            var getResult = await _roleServices.GetAsync(gid);
            if (getResult.code != 0)
            {
                return Json(getResult);
            }
            var role = getResult.data;
            if (role.IsDefault)
            {
                result.code = (int)StatusCodeEnum.IsDefault;
                result.msg = StatusCodeEnum.IsDefault.GetEnumText();
                return Json(result);
            }
            role.IsDelete = true;
            role.UpdateUserId = UserManager.Id;
            role.UpdateTime = DateTime.Now;
            var updateResult = await _roleServices.UpdateAsync(role);
            if (updateResult.code != 0)
            {
                return Json(updateResult);
            }
            return Json(result);
        }

        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        public IActionResult RoleMenu()
        {
            return View();
        }
        /// <summary>
        /// 角色授权
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RoleMenu([FromForm]string id,List<MenuButtonAttributes> menuButtonAttributes)
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
            if (!menuButtonAttributes.Any())
            {
                result.code = (int)StatusCodeEnum.ParameterError;
                result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                return Json(result);
            }
            var getRoleResult = await _roleServices.GetAsync(gid);
            if (getRoleResult.data == null)
            {
                result.code = (int)StatusCodeEnum.Unauthorized;
                result.msg = StatusCodeEnum.Unauthorized.GetEnumText();
                return Json(result);
            }
            var getAuthorizeResult = await _roleServices.AuthorizeAsync(new RoleMenuPara { RoleId= gid ,MenuButtonIds= menuButtonAttributes });
            if (getAuthorizeResult.code != 0)
            {
                return Json(getAuthorizeResult);
            }
            return Json(result);
        }
        /// <summary>
        /// 根据id获取所有菜单和按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetAllMenuButtonByRoleId([FromQuery]string id)
        {
            var result = new ListResult<Tree>();
            Guid gid;
            if (!Guid.TryParse(id, out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var getRoleResult = await _roleServices.GetAsync(gid);
            if (getRoleResult.data == null)
            {
                result.code = (int)StatusCodeEnum.Unauthorized;
                result.msg = StatusCodeEnum.Unauthorized.GetEnumText();
                return Json(result);
            }
            var getMenuButtonResult = await _roleServices.GetAllMenuButtonByRoleIdAsync(gid);
            if (getMenuButtonResult.code != 0)
            {
                return Json(getMenuButtonResult);
            }
            result.data = getMenuButtonResult.data;
            return Json(result);
        }
    }
}