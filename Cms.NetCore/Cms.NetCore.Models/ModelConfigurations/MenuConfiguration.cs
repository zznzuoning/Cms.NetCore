using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Cms.NetCore.Models.ModelConfigurations
{
    public class MenuConfiguration : IBaseConfiguration<Menu>
    {
        public override void Configures(EntityTypeBuilder<Menu> builder)
        {
            //设置属性
            builder.Property(d => d.Name).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Code).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Icon).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Sort).HasDefaultValue(1);
            builder.Property(d => d.BaseUrl).IsRequired().HasMaxLength(200);
            //映射
            builder.ToTable("Menu");
            builder.Property(d => d.ParentID).HasColumnName("ParentID");
            builder.Property(d => d.Name).HasColumnName("Name");
            builder.Property(d => d.Code).HasColumnName("Code");
            builder.Property(d => d.Icon).HasColumnName("Icon");
            builder.Property(d => d.BaseUrl).HasColumnName("BaseUrl");
            builder.Property(d => d.Sort).HasColumnName("Sort");
            //外键
            builder.HasOne(d => d.ParentMenu).WithMany(d=>d.ParentMenus).HasForeignKey(d => d.ParentID);
        }
    }
}
