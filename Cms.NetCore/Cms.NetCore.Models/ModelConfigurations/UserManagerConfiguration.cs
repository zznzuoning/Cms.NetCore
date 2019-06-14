using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public class UserManagerConfiguration : IBaseConfiguration<UserManager>
    {
        public override void Configures(EntityTypeBuilder<UserManager> builder)
        {
            //设置属性
            builder.Property(d => d.RealName).IsRequired().HasMaxLength(20);
            builder.Property(d => d.Remarks).HasMaxLength(50);
            //映射
            builder.ToTable("UserManager");
            builder.Property(d => d.RealName).HasColumnName("RealName");
            builder.Property(d => d.HeadImgUrl).HasColumnName("HeadImgUrl");
            builder.Property(d => d.Mobilephone).HasColumnName("Mobilephone");
            builder.Property(d => d.Email).HasColumnName("Email");
            builder.Property(d => d.IsEnabled).HasColumnName("IsEnabled");
            builder.Property(d => d.Remarks).HasColumnName("Remarks");
            builder.Property(d => d.UserLoginId).HasColumnName("UserLoginId");
            //外键
            builder.HasOne(d => d.UserLogin).WithOne(d=>d.UserManager);
        }
    }
}
