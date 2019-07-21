using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using Microsoft.EntityFrameworkCore;
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

        public List<Menu> GetMenusByUserId(Guid id)
        {
            var menu = ( from ur in EFContext.Context.Set<UserRole>()
                         join rmb in EFContext.Context.Set<RoleMenuButton>() on ur.RoleId equals rmb.RoleId into rmb_join
                         from rmb in rmb_join.DefaultIfEmpty()
                         join mb in EFContext.Context.Set<MenuButton>() on new { MenuButtonId= rmb.MenuButtonId } equals new { MenuButtonId= mb.Id } into mb_join
                         from mb in mb_join.DefaultIfEmpty()
                         join m in EFContext.Context.Set<Menu>() on new { MenuId =mb.MenuId } equals new { MenuId = m.Id } into m_join
                         from m in m_join.DefaultIfEmpty()
                         where
                           ur.UserManager.Id == id &&!m.IsDelete
                         select m ).Distinct();
            return menu.OrderBy(d => d.Sort).ToList();
        }

        public async Task<List<Menu>> GetMenusByUserIdAsync(Guid id)
        {
            var menu = ( from ur in EFContext.Context.Set<UserRole>()
                         join rmb in EFContext.Context.Set<RoleMenuButton>() on ur.RoleId equals rmb.RoleId into rmb_join
                         from rmb in rmb_join.DefaultIfEmpty()
                         join mb in EFContext.Context.Set<MenuButton>() on new { MenuButtonId = rmb.MenuButtonId } equals new { MenuButtonId = mb.Id } into mb_join
                         from mb in mb_join.DefaultIfEmpty()
                         join m in EFContext.Context.Set<Menu>() on new { MenuId = mb.MenuId } equals new { MenuId = m.Id } into m_join
                         from m in m_join.DefaultIfEmpty()
                         where
                           ur.UserManager.Id == id && !m.IsDelete
                         select m ).Distinct();
            return await menu.OrderBy(d => d.Sort).ToListAsync();
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
