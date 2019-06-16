using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public  void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            //设置属性
            builder.HasKey(d=>d.Id);
            builder.Property(d => d.UserName).IsRequired().HasMaxLength(20);
            builder.Property(d => d.PassWord).IsRequired();
            builder.Property(d => d.LastLoginTime).ValueGeneratedOnAddOrUpdate();
            //映射
            builder.ToTable("UserLogin");
            builder.Property(d => d.UserName).HasColumnName("UserName");
            builder.Property(d => d.PassWord).HasColumnName("PassWord");
            builder.Property(d => d.LogInCount).HasColumnName("LogInCount");
            builder.Property(d => d.LastLoginIp).HasColumnName("LastLoginIp");
            builder.Property(d => d.LastLoginTime).HasColumnName("LastLoginTime");
            
        }
    }
}
