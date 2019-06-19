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
using Cms.NetCore.ViewModels.param.Menu;
using Cms.NetCore.ViewModels.Results.Menu;
using Microsoft.AspNetCore.Mvc;

namespace Cms.NetCore.Admin.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuServices _menuServices;
        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取所有菜单数据
        /// </summary>
        /// <param name="menuSerach">查询条件</param>
        /// <returns></returns>
        public async Task<IActionResult> GetList([FromQuery]MenuSerach menuSerach)
        {
            if (!string.IsNullOrWhiteSpace(menuSerach.Name))
            {
                LinqComm<Menu>.And(d => d.Name.Contains(menuSerach.Name));
            }
            var getResult = await _menuServices.GetListPagedAsync(Specification<Menu>.Eval(LinqComm<Menu>.GetExpression()), d => d.Sort, SortOrder.Ascending, menuSerach.Page, menuSerach.Limit);
            var menuList = getResult.data.Select(d => new MenuList
            {
                Id = d.Id,
                MenuName = d.Name,
                Sid = d.Sid,
                ParentMenuName = d.ParentMenu?.Name,
                Code = d.Code,
                Icon = d.Icon,
                Url = d.BaseUrl,
                UpdateTime = d.UpdateTime,
                UpdateUser = d.UpdateUser?.RealName,
                Sort = d.Sort
            }).ToList();

            return Json(new PageResult<MenuList>
            {
                code = getResult.code,
                msg = getResult.msg,
                count = getResult.count,
                data = menuList
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
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
            var getResult = await _menuServices.GetAsync(gid);

            if (getResult.code != 0)
            {
                return Json(getResult);
            }
            var menu = getResult.data;
            if (menu.ParentId == null)
            {
                result.code = (int)StatusCodeEnum.IsDefault;
                result.msg = StatusCodeEnum.IsDefault.GetEnumText();
                return Json(result);
            }
            menu.IsDelete = true;
            //先把数据库的数据加载出来，避免每次都去查数据库
            var childrenMenus = menu.ChildrenMenus.ToList();
            if (childrenMenus.Any())
            { //递归要删除的子节点
                menu.ChildrenMenus = UpdateChildren(childrenMenus);
            }
            var updateResult = await _menuServices.UpdateAsync(menu);
            if (updateResult.code != 0)
            {
                return Json(updateResult);
            }
            return Json(result);
        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="menus"></param>
        /// <returns></returns>
        private ICollection<Menu> UpdateChildren(ICollection<Menu> menus)
        {
            foreach (var item in menus.Where(d => !d.IsDelete))
            {

                item.IsDelete = true;
                if (item.ChildrenMenus.Any())
                {
                    item.ChildrenMenus = UpdateChildren(item.ChildrenMenus);
                }

            }
            return menus;
        }
    }
}