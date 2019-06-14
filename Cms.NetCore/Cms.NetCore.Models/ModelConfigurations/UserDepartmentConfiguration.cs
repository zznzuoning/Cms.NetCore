using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public class UserDepartmentConfiguration : IEntityTypeConfiguration<UserDepartment>
    {
        public void Configure(EntityTypeBuilder<UserDepartment> builder)
        {
            //设置属性
            builder.HasKey(d => d.Id);
            builder.Property(d => d.UserManagerId).IsRequired();
            builder.Property(d => d.DepartmentId).IsRequired();
            //映射
            builder.ToTable("UserDepartment");
            builder.Property(d => d.Id).HasColumnName("Id");
            builder.Property(d => d.UserManagerId).HasColumnName("UserManagerId");
            builder.Property(d => d.DepartmentId).HasColumnName("DepartmentId");
            //外键
            builder.HasOne(d => d.UserManager)
                .WithMany(d => d.UserDepartments)
                .HasForeignKey(d => d.UserManagerId);
            builder.HasOne(d => d.Department)
                .WithMany(d=>d.UserDepartments)
                .HasForeignKey(d=>d.DepartmentId);
        }
    }
}
