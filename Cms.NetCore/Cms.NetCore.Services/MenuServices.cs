using Cms.NetCore.Infrastructure.enums;
using Cms.NetCore.Infrastructure.Extension;
using Cms.NetCore.Infrastructure.Specifications;
using Cms.NetCore.IRepository;
using Cms.NetCore.IServices;
using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Menu;
using Cms.NetCore.ViewModels.Results.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.Services
{
    public class MenuServices : ApplicationServices<Menu>, IMenuServices
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMenuButtonRepository _menuButtonRepository;
        public MenuServices(IBaseRepository<Menu> baseRepository, IMenuRepository menuRepository, IMenuButtonRepository menuButtonRepository) : base(baseRepository)
        {
            _menuRepository = menuRepository;
            _menuButtonRepository = menuButtonRepository;
        }

        public DataResult<MenuButtonList> GetButtonByMenuId(Guid id)
        {
            var result = new DataResult<MenuButtonList>();
           
                List<MenuButton> menuButtons = _menuButtonRepository.GetList(Specification<MenuButton>.Eval(d => d.MenuId == id));
                result.data.ButtonIds = menuButtons.Select(d => d.ButtonId).ToArray();
                return result;
            
            
        }

        public async Task<DataResult<MenuButtonList>> GetButtonByMenuIdAsync(Guid id)
        {
            var result = new DataResult<MenuButtonList>();
            
                List<MenuButton> menuButtons = await _menuButtonRepository.GetListAsync(Specification<MenuButton>.Eval(d => d.MenuId == id));
                var buttonids = menuButtons.Select(d => d.ButtonId).ToArray();
                result.data = new MenuButtonList { ButtonIds = buttonids };
                return result;
            
          
        }

        public ListResult<UserMenuTree> GetMenusByUserId(Guid id)
        {
            var result = new ListResult<UserMenuTree>();
           
                List<Menu> menus = _menuRepository.GetMenusByUserId(id);
                Guid?[] parentIds = menus.Select(d => d.ParentId).Distinct().ToArray();
                var parentMenus = _menuRepository.GetList(Specification<Menu>.Eval(d => parentIds.Any(x => x == d.Id)));
                var unionMenus = menus.Union(parentMenus);
                var treeList = new List<UserMenuTree>();
                foreach (var item in unionMenus.Where(d=>d.ParentId==null))
                {
                    var tree = new UserMenuTree();
                    tree.Id = item.Id;
                    tree.Title = item.Name;
                    tree.Icon = item.Icon;
                    tree.Href = item.BaseUrl;
                    tree.Spread = true;
                    tree.Children = Recursion(unionMenus.ToList(), item.Id);
                    treeList.Add(tree);
                }
                result.data = treeList;
                return result;
            
           
        }
        private List<UserMenuTree> Recursion(List<Menu> menuList,Guid? id)
        {
            var treeList = new List<UserMenuTree>();
            var menus = menuList.Where(d => d.ParentId == id);
            foreach (var menu in menus)
            {
                var tree = new UserMenuTree();
                tree.Id = menu.Id;
                tree.Title = menu.Name;
                tree.Icon = menu.Icon;
                tree.Href = menu.BaseUrl;
                tree.Children = Recursion(menuList, menu.Id);
                treeList.Add(tree);
            }
            return treeList;
        }
        public async Task<ListResult<UserMenuTree>> GetMenusByUserIdAsync(Guid id)
        {
            var result = new ListResult<UserMenuTree>();
           
                List<Menu> menus = await _menuRepository.GetMenusByUserIdAsync(id);
                Guid?[] parentIds = menus.Select(d => d.ParentId).Distinct().ToArray();
                var parentMenus =await _menuRepository.GetListAsync(Specification<Menu>.Eval(d => parentIds.Any(x => x == d.Id)));
                var unionMenus = menus.Union(parentMenus);
                var treeList = new List<UserMenuTree>();
                foreach (var item in unionMenus.Where(d => d.ParentId == null))
                {
                    var tree = new UserMenuTree();
                    tree.Id = item.Id;
                    tree.Title = item.Name;
                    tree.Icon = item.Icon;
                    tree.Href = item.BaseUrl;
                    tree.Spread = true;
                    tree.Children = Recursion(unionMenus.ToList(), item.Id);
                    treeList.Add(tree);
                }
                result.data = treeList;
                return result;
            
           
        }

        public Result SetMenuButton(MenuButtonModel menuButton)
        {

            var result = new Result();
            
                if (menuButton == null)
                {
                    result.code = (int)StatusCodeEnum.ParameterError;
                    result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                    return result;
                }
                var menuButtonList = new List<MenuButton>();
                foreach (var buttonid in menuButton.ButtonIds.Split(','))
                {
                    menuButtonList.Add(new MenuButton
                    {
                        MenuId = Guid.Parse(menuButton.MenuId),
                        ButtonId = Guid.Parse(buttonid)
                    });
                }
                int isSet = _menuRepository.SetMenuButton(menuButtonList);
                if (isSet == 0)
                {
                    result.code = (int)StatusCodeEnum.Accepted;
                    result.msg = StatusCodeEnum.Accepted.GetEnumText();
                }
                return result;
            
            

        }

        public async Task<Result> SetMenuButtonAsync(MenuButtonModel menuButton)
        {
            var result = new Result();
           
                if (menuButton == null)
                {
                    result.code = (int)StatusCodeEnum.ParameterError;
                    result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                    return result;
                }
                var menuButtonList = new List<MenuButton>();
                foreach (var buttonid in menuButton.ButtonIds.Split(','))
                {
                    menuButtonList.Add(new MenuButton
                    {
                        MenuId = Guid.Parse(menuButton.MenuId),
                        ButtonId = Guid.Parse(buttonid)
                    });
                }
                int isSet = await _menuRepository.SetMenuButtonAsync(menuButtonList);
                if (isSet == 0)
                {
                    result.code = (int)StatusCodeEnum.Accepted;
                    result.msg = StatusCodeEnum.Accepted.GetEnumText();
                }
                return result;
            
           

        }
    }
}
