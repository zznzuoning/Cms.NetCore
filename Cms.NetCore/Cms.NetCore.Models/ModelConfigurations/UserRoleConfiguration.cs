using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //设置属性
            builder.HasKey(d => d.Id);
            builder.Property(d => d.RoleId).IsRequired();
            builder.Property(d => d.UserManagerId).IsRequired();
            //映射
            builder.ToTable("UserRole");
            builder.Property(d => d.Id).HasColumnName("Id");
            builder.Property(d => d.RoleId).HasColumnName("RoleId");
            builder.Property(d => d.UserManagerId).HasColumnName("UserManagerId");
            //外键
            builder.HasOne(d => d.UserManager)
                .WithMany(d => d.UserRoles)
                .HasForeignKey(d => d.UserManagerId);
            builder.HasOne(d => d.Role)
                .WithMany(d => d.UserRoles)
                .HasForeignKey(d => d.RoleId);
        }
    }
}
