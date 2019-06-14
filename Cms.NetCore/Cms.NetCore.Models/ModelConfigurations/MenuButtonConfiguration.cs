using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public class MenuButtonConfiguration : IEntityTypeConfiguration<MenuButton>
    {
        public void Configure(EntityTypeBuilder<MenuButton> builder)
        {
            //设置属性
            builder.HasKey(d => d.Id);
            builder.Property(d => d.MenuId).IsRequired();
            builder.Property(d => d.ButtonId).IsRequired();
            //映射
            builder.ToTable("MenuButton");
            builder.Property(d => d.Id).HasColumnName("Id");
            builder.Property(d => d.MenuId).HasColumnName("MenuId");
            builder.Property(d => d.ButtonId).HasColumnName("ButtonId");
            //外键
            builder.HasOne(d => d.Menu)
                .WithMany(d => d.MenuButtons)
                .HasForeignKey(d => d.MenuId);
         
        }
    }
}
