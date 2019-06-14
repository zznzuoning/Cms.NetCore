using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public class RoleMenuButtonConfiguration : IEntityTypeConfiguration<RoleMenuButton>
    {
        public void Configure(EntityTypeBuilder<RoleMenuButton> builder)
        {
            //设置属性
            builder.HasKey(d => d.Id);
            builder.Property(d => d.RoleId).IsRequired();
            builder.Property(d => d.MenuButtonId).IsRequired();
            //映射
            builder.ToTable("RoleMenuButton");
            builder.Property(d => d.Id).HasColumnName("Id");
            builder.Property(d => d.RoleId).HasColumnName("RoleId");
            builder.Property(d => d.MenuButtonId).HasColumnName("MenuButtonId");
            //外键
            builder.HasOne(d => d.Role)
                .WithMany(d=>d.RoleMenuButtons)
                .HasForeignKey(d=>d.RoleId);
            builder.HasOne(d => d.MenuButton)
                .WithOne();
        }
    }
}
