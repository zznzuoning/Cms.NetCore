using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cms.NetCore.Models;
namespace Cms.NetCore.IRepository
{
    public interface IMenuRepository:IBaseRepository<Menu>
    {
        int SetMenuButton(List<MenuButton> menuButtons);
        Task<int> SetMenuButtonAsync(List<MenuButton> menuButtons);
        List<Menu> GetMenusByUserId(Guid id);
        Task<List<Menu>> GetMenusByUserIdAsync(Guid id);
    }
}
