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
            try
            {
                List<MenuButton> menuButtons = _menuButtonRepository.GetList(Specification<MenuButton>.Eval(d=>d.MenuId==id));
                result.data.ButtonIds= menuButtons.Select(d => d.ButtonId).ToArray();
                return result;
            }
            catch (Exception ex)
            {
                ex.Source = this.GetType().Name;
                result.code = (int)StatusCodeEnum.Error;
                result.msg = $"{ex.Source}出现异常,请联系管理员";
                return result;
            }
        }

        public async Task<DataResult<MenuButtonList>> GetButtonByMenuIdAsync(Guid id)
        {
            var result = new DataResult<MenuButtonList>();
            try
            {
                List<MenuButton> menuButtons = await _menuButtonRepository.GetListAsync(Specification<MenuButton>.Eval(d => d.MenuId == id));
                var buttonids = menuButtons.Select(d => d.ButtonId).ToArray();
                result.data=new MenuButtonList { ButtonIds= buttonids };
                return result;
            }
            catch (Exception ex)
            {
                ex.Source = this.GetType().Name;
                result.code = (int)StatusCodeEnum.Error;
                result.msg = $"{ex.Source}出现异常,请联系管理员";
                return result;
            }
        }

        public Result SetMenuButton(MenuButtonModel menuButton)
        {

            var result = new Result();
            try
            {
                if (menuButton == null)
                {
                    result.code = (int)StatusCodeEnum.ParameterError;
                    result.msg = StatusCodeEnum.ParameterError.GetEnumText();
                    return result;
                }
                var menuButtonList = new List<MenuButton>();
                foreach (var buttonid in menuButton.ButtonIds.Split(','))
                {
                    menuButtonList.Add(new MenuButton {
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
            catch (Exception ex)
            {
                ex.Source = this.GetType().Name;
                result.code = (int)StatusCodeEnum.Error;
                result.msg = $"{ex.Source}出现异常,请联系管理员";
                return result;
            }

        }

        public async Task<Result> SetMenuButtonAsync(MenuButtonModel menuButton)
        {
            var result = new Result();
            try
            {
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
            catch (Exception ex)
            {
                ex.Source = this.GetType().Name;
                result.code = (int)StatusCodeEnum.Error;
                result.msg = $"{ex.Source}出现异常,请联系管理员";
                return result;
            }

        }
    }
}
