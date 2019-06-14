using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Cms.NetCore.Models.ModelConfigurations
{
    public class ButtionConfiguration : IBaseConfiguration<Buttion>
    {
        public override void Configures(EntityTypeBuilder<Buttion> builder)
        {
            //设置属性
            builder.Property(d => d.Name).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Code).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Icon).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Sort).HasDefaultValue(1);
            builder.Property(d => d.Description).HasMaxLength(200);
            //映射
            builder.ToTable("Buttion");
            builder.Property(d => d.Name).HasColumnName("Name");
            builder.Property(d => d.Code).HasColumnName("Code");
            builder.Property(d => d.Icon).HasColumnName("Icon");
            builder.Property(d => d.Sort).HasColumnName("Sort");
            builder.Property(d => d.Description).HasColumnName("Description");
          
        }
    }
}
