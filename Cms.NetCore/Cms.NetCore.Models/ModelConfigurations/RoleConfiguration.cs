using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public class RoleConfiguration : IBaseConfiguration<Role>
    {
        public RoleConfiguration():base()
        { }

        
        public override void Configures(EntityTypeBuilder<Role> builder)
        {
          
            //设置属性
         
            builder.Property(d => d.Name).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Remarks).HasMaxLength(50);
            //表映射
            builder.ToTable("Role");
            builder.Property(d => d.Name).HasColumnName("RoleName");
            builder.Property(d => d.IsDefault).HasColumnName("IsDefault");
            builder.Property(d => d.Remarks).HasColumnName("Remarks");
        }
    }
}
