using Cms.NetCore.Models;
using Cms.NetCore.ViewModels;
using Cms.NetCore.ViewModels.param.Menu;
using Cms.NetCore.ViewModels.Results.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.IServices
{
    public interface IMenuServices : IApplicationServices<Menu>
    {
        Result SetMenuButton(MenuButtonModel menuButton);
        Task<Result> SetMenuButtonAsync(MenuButtonModel menuButton);
        DataResult<MenuButtonList> GetButtonByMenuId(Guid id);
        Task<DataResult<MenuButtonList>> GetButtonByMenuIdAsync(Guid id);
    }
}
