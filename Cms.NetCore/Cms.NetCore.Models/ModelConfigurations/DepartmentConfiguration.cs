using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
namespace Cms.NetCore.Models.ModelConfigurations
{
    public class DepartmentConfiguration : IBaseConfiguration<Department>
    {
        public override void Configures(EntityTypeBuilder<Department> builder)
        {
            //设置属性
            builder.Property(d => d.Name).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Sort).HasDefaultValue(1);
            //映射
            builder.ToTable("Department");
            builder.Property(d => d.ParentId).HasColumnName("ParentId");
            builder.Property(d => d.Name).HasColumnName("Name");
            builder.Property(d => d.Sort).HasColumnName("Sort");
            //外键
            builder.HasOne(d => d.Departments)
                .WithMany(d => d.Departmentss)
                .HasForeignKey(d => d.ParentId);
        }
    }
}
