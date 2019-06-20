using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.Repository
{
    public class MenuRepository : EntityFrameworkRepository<Menu>, IMenuRepository
    {
        public MenuRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }

        public int SetMenuButton(List<MenuButton> menuButtons)
        {
            var menuButton = EFContext.Context.Set<MenuButton>().Where(d => d.MenuId == menuButtons.FirstOrDefault().MenuId);
            EFContext.Context.Set<MenuButton>().RemoveRange(menuButton);
            EFContext.Context.Set<MenuButton>().AddRange(menuButtons);

            return EFContext.Context.SaveChanges();
        }

        public async Task<int> SetMenuButtonAsync(List<MenuButton> menuButtons)
        {
            var menuButton = EFContext.Context.Set<MenuButton>().Where(d => d.MenuId == menuButtons.FirstOrDefault().MenuId);
            EFContext.Context.Set<MenuButton>().RemoveRange(menuButton);
            EFContext.Context.Set<MenuButton>().AddRange(menuButtons);

            return await EFContext.Context.SaveChangesAsync();
        }
    }
}
