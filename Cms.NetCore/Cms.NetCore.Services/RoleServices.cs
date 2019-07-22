using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using Cms.NetCore.IRepository;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Role;
using Cms.NetCore.ViewModels.Results.MenuButton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.Services
{
    public class RoleServices : ApplicationServices<Role>, IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        public RoleServices(IBaseRepository<Role> baseRepository, IRoleRepository roleRepository) : base(baseRepository)
        {
            _roleRepository = roleRepository;
        }


        public ListResult<Tree> GetAllMenuButtonByRoleId(Guid id)
        {
            var result = new ListResult<Tree>();

            List<RoleMenuButtonList> data = _roleRepository.GetAllMenuButtonByRoleId(id);
            result.data = GetAllMenuButtonByRoleIdSelect(data, id);
            return result;


        }
        private List<Tree> GetAllMenuButtonByRoleIdSelect(List<RoleMenuButtonList> data, Guid id)
        {
            List<Tree> list = new List<Tree>();
            var distinctMenuData = data.GroupBy(d => d.MenuId).Select(d => new DistinctMenu
            {
                Id = d.Key,
                Name = d.Max(x => x.MenuName),
                ParentId = d.Max(x => x.ParentId)
            }).ToList();
            var topData = data.Where(d => d.ParentId == null);
            foreach (var item in topData)
            {

                var menuButton = new Tree();
                var attributes = new MenuButtonAttributes();
                menuButton.Id = item.MenuId;
                menuButton.Title = item.MenuName;
                attributes.MenuButtonId = null;
                menuButton.Attributes = attributes;
                menuButton.Spread = true;
                menuButton.Children = RecursionMenuButton(data, distinctMenuData, item.MenuId, id);
                list.Add(menuButton);
            }
            return list;
        }

        private List<MenuTree> RecursionMenuButton(List<RoleMenuButtonList> data, List<DistinctMenu> menuData, Guid menuId, Guid roleId)
        {
            var list = new List<MenuTree>();
            var childMenu = menuData.Where(d => d.ParentId == menuId);
            foreach (var menu in childMenu)
            {
                var buttonList = new List<ButtonTree>();
                var menuButton = new MenuTree();
                var attributes = new MenuButtonAttributes();
                menuButton.Id = menu.Id;
                menuButton.Title = menu.Name;
                attributes.MenuButtonId = null;
                menuButton.Attributes = attributes;
                var buttonTree = data.Where(d => d.MenuId == menu.Id && d.ButtonId.HasValue);
                if (buttonTree.Any())
                {
                    foreach (var button in buttonTree)
                    {
                        var buttons = new ButtonTree();
                        var buttonAttribute = new MenuButtonAttributes();
                        buttons.Id = roleId;
                        buttons.Title = button.ButtonName;
                        buttons.Checked = button.Checked;
                        buttons.Attributes = buttonAttribute;
                        buttonAttribute.MenuButtonId = button.MenuButtonId;
                        buttonList.Add(buttons);
                    }
                }
                menuButton.Children = buttonList;
                list.Add(menuButton);
            }
            return list;
        }
        public async Task<ListResult<Tree>> GetAllMenuButtonByRoleIdAsync(Guid id)
        {
            var result = new ListResult<Tree>();

            List<RoleMenuButtonList> data = await _roleRepository.GetAllMenuButtonByRoleIdAsync(id);
            result.data = GetAllMenuButtonByRoleIdSelect(data, id);
            return result;


        }

        public Result Authorize(RoleMenuPara roleMenuPara)
        {
            var result = new Result();

            bool isDel = _roleRepository.DelRoleMenuButtonByRoleId(roleMenuPara.RoleId);
            if (isDel)
            {
                if (roleMenuPara.MenuButtonIds != null)
                {
                    List<RoleMenuButton> roleMenuButtonList = new List<RoleMenuButton>();
                    foreach (var item in roleMenuPara.MenuButtonIds)
                    {
                        var roleMenuButton = new RoleMenuButton
                        {
                            Id = Guid.NewGuid(),
                            RoleId = roleMenuPara.RoleId,
                            MenuButtonId = item.MenuButtonId.Value
                        };
                        roleMenuButtonList.Add(roleMenuButton);
                    }
                    bool isAdd = _roleRepository.Authorize(roleMenuButtonList);
                    if (!isAdd)
                    {
                        result.code = (int)StatusCodeEnum.Accepted;
                        result.msg = StatusCodeEnum.Accepted.GetEnumText();
                        return result;
                    }
                }
                else
                {
                    result.code = (int)StatusCodeEnum.ParameterError;
                    result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                    return result;
                }

            }
            else
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
                return result;
            }

            return result;

        }

        public async Task<Result> AuthorizeAsync(RoleMenuPara roleMenuPara)
        {
            var result = new Result();

            bool isDel = await _roleRepository.DelRoleMenuButtonByRoleIdAsync(roleMenuPara.RoleId);
            if (isDel)
            {
                if (roleMenuPara.MenuButtonIds.Any())
                {
                    List<RoleMenuButton> roleMenuButtonList = new List<RoleMenuButton>();
                    foreach (var item in roleMenuPara.MenuButtonIds)
                    {
                        var roleMenuButton = new RoleMenuButton
                        {
                            Id = Guid.NewGuid(),
                            RoleId = roleMenuPara.RoleId,
                            MenuButtonId = item.MenuButtonId.Value
                        };
                        roleMenuButtonList.Add(roleMenuButton);
                    }
                    bool isAdd = await _roleRepository.AuthorizeAsync(roleMenuButtonList);
                    if (!isAdd)
                    {
                        result.code = (int)StatusCodeEnum.Accepted;
                        result.msg = StatusCodeEnum.Accepted.GetEnumText();
                        return result;
                    }
                }
                else
                {
                    result.code = (int)StatusCodeEnum.ParameterError;
                    result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                    return result;
                }

            }
            else
            {
                result.code = (int)StatusCodeEnum.Accepted;
                result.msg = StatusCodeEnum.Accepted.GetEnumText();
                return result;
            }

            return result;


        }
    }
}
