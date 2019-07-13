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
        private readonly IButtionServices _buttionServices;
        public MenuController(IMenuServices menuServices, IButtionServices buttionServices)
        {
            _menuServices = menuServices;
            _buttionServices = buttionServices;
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
                Sort = d.Sort,
                IsHasChildren = d.ChildrenMenus.Any(x=>!x.IsDelete)
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
        /// 根据id获取菜单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetMenuById([FromQuery]string id)
        {
            var result = new DataResult<MenuModel>();
            Guid gid;
            if (!Guid.TryParse(id, out gid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var getResult = await _menuServices.GetAsync(gid);

            if (getResult.data != null)
            {
                var menu = new MenuModel();
                menu.Id = getResult.data.Id.ToString();
                menu.MenuName = getResult.data.Name;
                menu.Code = getResult.data.Code;
                menu.Icon = getResult.data.Icon;
                menu.Sort = getResult.data.Sort;
                menu.ParentId = getResult.data.ParentId.ToString();
                menu.Url = getResult.data.BaseUrl;
                result.data = menu;
                result.code = getResult.code;
                result.msg = getResult.msg;
            }
            return Json(result);
        }
        public IActionResult CreateOrUpdate()
        {
            return View();
        }
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="menuAddOrUpdate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate([FromForm] MenuAddOrUpdate menuAddOrUpdate)
        {
            var result = new Result();
            Guid gid = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(menuAddOrUpdate.Id))
            {
                if (!Guid.TryParse(menuAddOrUpdate.Id, out gid))
                {
                    result.code = (int)StatusCodeEnum.HttpMehtodError;
                    result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                    return Json(result);
                }
            }
            Guid pid = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(menuAddOrUpdate.ParentId))
            {
                if (!Guid.TryParse(menuAddOrUpdate.ParentId, out pid))
                {
                    result.code = (int)StatusCodeEnum.HttpMehtodError;
                    result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                    return Json(result);
                }
            }
            var getResult = await _menuServices.GetAsync(gid);
            var menu = getResult.data ?? new Menu();
            menu.Name = menuAddOrUpdate.MenuName;
            menu.Code = menuAddOrUpdate.Code;
            menu.Icon = menuAddOrUpdate.Icon;
            menu.Sort = menuAddOrUpdate.Sort;
            menu.BaseUrl = menuAddOrUpdate.Url;
            if (pid != Guid.Empty)
            {
                menu.ParentId = pid;
            }
            if (menu.Id == Guid.Empty)
            {
                var insertResult = await _menuServices.InsertAsync(menu);
                if (insertResult.code != 0)
                {
                    return Json(insertResult);
                }
            }
            else
            {
                var updateResult = await _menuServices.UpdateAsync(menu);
                if (updateResult.code != 0)
                {
                    return Json(updateResult);
                }
            }
            result.code = (int)StatusCodeEnum.Success;
            result.msg = StatusCodeEnum.Success.GetEnumText();
            return Json(result);
        }

        public async Task<IActionResult> GetTreeList()
        {
            var getResult = await _menuServices.GetListAsync(Specification<Menu>.Eval(d => !d.IsDelete));
            var menuList = getResult.data;
            var topMenu = menuList.Where(d => d.ParentId == null).ToList();
            var treeList = new List<SelectTree>();
            //foreach (var menu in topMenu)
            //{
            //    var tree = new Tree();
            //    tree.Id = menu.Id;
            //    tree.Name = menu.Name;
            //    tree.Open = true;
            //    tree.Children = GetChildren(menuList,menu.Id);
            //    treeList.Add(tree);
            //}
            foreach (var menu in topMenu)
            {
                var tree = new SelectTree();
                tree.Id = menu.Id;
                tree.Name = menu.Name;
                tree.Open = true;
                tree.Children = GetChildrens(menuList, menu.Id);
                treeList.Add(tree);
            }
            return Json(treeList);
        }
        /// <summary>
        /// 递归
        /// </summary>
        /// <param name="menuList"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<SelectTree> GetChildren(List<Menu> menuList, Guid id)
        {
            var treeList = new List<SelectTree>();
            var menus = menuList.Where(d => d.ParentId == id);
            foreach (var menu in menus)
            {
                var tree = new SelectTree();
                tree.Id = menu.Id;
                tree.Name = menu.Name;
                tree.Children = GetChildren(menuList, menu.Id);
                treeList.Add(tree);
            }
            return treeList;
        }
        private List<SelectTree> GetChildrens<T>(List<T> menuList, Guid id) where T : BaseMenu
        {

            var treeList = new List<SelectTree>();
            var menus = menuList.Where(d => d.ParentId == id);
            foreach (var menu in menus)
            {
                var tree = new SelectTree();
                var menutype = menu.GetType().GetProperties();
                foreach (var items in tree.GetType().GetProperties())
                {
                    foreach (var item in menutype)
                    {
                        if (item.Name == items.Name)
                        {
                            items.SetValue(tree, item.GetValue(menu));
                            break;
                        }

                    }
                }
                tree.Children = GetChildrens(menuList, menu.Id);





                treeList.Add(tree);
            }
            return treeList;
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

        /// <summary>
        /// 分配按钮
        /// </summary>
        /// <returns></returns>
        public IActionResult SetMenuButton()
        {
            return View();
        }
        /// <summary>
        /// 根据菜单id获取按钮
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetButtonByMenuId([FromQuery]string id)
        {
            var result = new DataResult<MenuButtonList>
            {
                code = (int)StatusCodeEnum.Success,
                msg = StatusCodeEnum.Success.GetEnumText()
            };
            Guid mid = Guid.Empty;
            if (!Guid.TryParse(id, out mid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var getResult = await _menuServices.GetButtonByMenuIdAsync(mid);
            if (getResult.code != 0)
            {
                return Json(getResult);
            }
            result.data = getResult.data;
            return Json(result);
        }
        /// <summary>
        /// 分配按钮
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SetMenuButton([FromForm]MenuButtonModel menuButtonModel)
        {
            var result = new Result
            {
                code = (int)StatusCodeEnum.Success,
                msg = StatusCodeEnum.Success.GetEnumText()
            };
            Guid mid = Guid.Empty;
            if (!Guid.TryParse(menuButtonModel.MenuId, out mid))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            if (string.IsNullOrWhiteSpace(menuButtonModel.ButtonIds))
            {
                result.code = (int)StatusCodeEnum.HttpMehtodError;
                result.msg = StatusCodeEnum.HttpMehtodError.GetEnumText();
                return Json(result);
            }
            var setResult = await _menuServices.SetMenuButtonAsync(menuButtonModel);
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
        public async Task<IActionResult> GetButton(Guid id)
        {
            var getResult = await _buttionServices.GetListAsync(Specification<Button>.Eval(d => !d.IsDelete));
            var buttons = getResult.data.Select(d => new Select
            {
                value = d.Id.ToString(),
                Name = d.Name,
                selected = d.MenuButtons.Any(x => x.ButtonId == d.Id && x.MenuId == id)
            }).ToList();
            return Json(buttons);
        }

    }
}