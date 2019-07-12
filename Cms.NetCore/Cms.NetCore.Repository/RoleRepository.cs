using Cms.NetCore.IRepository;
using Cms.NetCore.Models;
using Cms.NetCore.ViewModels.Results.Menu;
using Cms.NetCore.ViewModels.Results.MenuButton;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.NetCore.Repository
{
    public class RoleRepository : EntityFrameworkRepository<Role>, IRoleRepository
    {
        public RoleRepository(IEntityFrameworkRepositoryContext Context) : base(Context)
        {
        }

        public List<RoleMenuButtonList> GetAllMenuButtonByRoleId(Guid id) => GetAllMenuButtonByRoleIdSelect(id).ToList();
        private IQueryable<RoleMenuButtonList> GetAllMenuButtonByRoleIdSelect(Guid id)
        {
            var data = from m in EFContext.Context.Set<Menu>()
                       join mb in EFContext.Context.Set<MenuButton>() on new { Id = m.Id } equals new { Id = mb.MenuId } into mb_join
                       from mb in mb_join.DefaultIfEmpty()
                       join b in EFContext.Context.Set<Button>() on new { ButtonId = mb.ButtonId } equals new { ButtonId = b.Id } into b_join
                       from b in b_join.DefaultIfEmpty()
                       join rmb in EFContext.Context.Set<RoleMenuButton>()
                             on new { MenuButtonId = mb.Id, RoleId = id }
                         equals new { rmb.MenuButtonId, RoleId = rmb.RoleId } into rmb_join
                       from rmb in rmb_join.DefaultIfEmpty()
                       orderby
                         m.Sort,
                         b.Sort
                       select new RoleMenuButtonList
                       {
                           MenuId = m.Id,
                           MenuName = m.Name,
                           ParentId = m.ParentId,
                           ButtonId = mb.ButtonId,
                           ButtonName = b.Name,
                           RoleId = rmb.RoleId,
                           MenuButtonId=mb.Id,
                           Checked =
                         rmb.MenuButtonId == null ? false : true
                       };
            return data;

        }
        public async Task<List<RoleMenuButtonList>> GetAllMenuButtonByRoleIdAsync(Guid id) => await GetAllMenuButtonByRoleIdSelect(id).ToListAsync();

        public bool DelRoleMenuButtonByRoleId(Guid id)
        {
            var roleMenubuttons = EFContext.Context.Set<RoleMenuButton>().Where(d => d.RoleId == id);
            if (roleMenubuttons.Any())
            {
                if (roleMenubuttons.Count() > 1)
                {
                    EFContext.Context.Set<RoleMenuButton>().RemoveRange(roleMenubuttons);
                }
                else
                {
                    EFContext.Context.Set<RoleMenuButton>().Remove(roleMenubuttons.FirstOrDefault());
                }
            }
            else
            {
                return true;
            }

            return EFContext.Context.SaveChanges() > 0;
        }
       
        public async Task<bool> DelRoleMenuButtonByRoleIdAsync(Guid id)
        {
            var roleMenubuttons = EFContext.Context.Set<RoleMenuButton>().Where(d => d.RoleId == id);
            if (roleMenubuttons.Any())
            {
                if (roleMenubuttons.Count() > 1)
                {
                    EFContext.Context.Set<RoleMenuButton>().RemoveRange(roleMenubuttons);
                }
                else
                {
                    EFContext.Context.Set<RoleMenuButton>().Remove(roleMenubuttons.FirstOrDefault());
                }
            }
            else
            {
                return true;
            }

            return await EFContext.Context.SaveChangesAsync() > 0;
        }

        public bool Authorize(List<RoleMenuButton> roleMenuButton)
        {
             EFContext.Context.Set<RoleMenuButton>().AddRange(roleMenuButton);
            return  EFContext.Context.SaveChanges()> 0;
        }

        public async Task<bool> AuthorizeAsync(List<RoleMenuButton> roleMenuButton)
        {
           await  EFContext.Context.Set<RoleMenuButton>().AddRangeAsync(roleMenuButton);
            return await EFContext.Context.SaveChangesAsync() > 0;
        }
    }
}
