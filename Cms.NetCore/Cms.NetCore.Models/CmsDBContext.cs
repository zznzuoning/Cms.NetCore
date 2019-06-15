using Cms.NetCore.Models.ModelConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models
{
    public class CmsDBContext : DbContext
    {
       

        //public DbSet<Role> Role { get; set; }
        public CmsDBContext(DbContextOptions<CmsDBContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleConfiguration())
                .ApplyConfiguration(new UserManagerConfiguration())
             .ApplyConfiguration(new UserLoginConfiguration())
             .ApplyConfiguration(new ButtionConfiguration())
             .ApplyConfiguration(new DepartmentConfiguration())
             .ApplyConfiguration(new MenuButtonConfiguration())
             .ApplyConfiguration(new MenuConfiguration())
             .ApplyConfiguration(new OperationalLogConfiguration())
             .ApplyConfiguration(new RoleMenuButtonConfiguration())
             .ApplyConfiguration(new UserDepartmentConfiguration())
             .ApplyConfiguration(new UserRoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
