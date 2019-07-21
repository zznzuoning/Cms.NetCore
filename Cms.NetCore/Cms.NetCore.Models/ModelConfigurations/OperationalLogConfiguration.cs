using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public class OperationalLogConfiguration : IEntityTypeConfiguration<OperationalLog>
    {
        public void Configure(EntityTypeBuilder<OperationalLog> builder)
        {
            //属性
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Controller).IsRequired().HasMaxLength(30);
            builder.Property(d => d.Action).IsRequired().HasMaxLength(20);
            builder.Property(d => d.OperationalTime).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
            builder.Property(d => d.OperationalIp).IsRequired().HasMaxLength(30);
            builder.Property(d => d.OperationalName).HasMaxLength(50);
            builder.Property(d => d.OperationalState).IsRequired().HasDefaultValue(0);
            //映射
            builder.ToTable("OperationalLog");
            builder.Property(d => d.Controller).HasColumnName("Controller");
            builder.Property(d => d.Action).HasColumnName("Action");
            builder.Property(d => d.UserManagerId).HasColumnName("UserManagerId");
            builder.Property(d => d.OperationalTime).HasColumnName("OperationalTime");
            builder.Property(d => d.OperationalIp).HasColumnName("OperationalIp");
            builder.Property(d => d.OperationalName).HasColumnName("OperationalName");
            builder.Property(d => d.OperationalState).HasColumnName("OperationalState");
            //外键
            builder.HasOne(d => d.UserManager)
                .WithMany(d => d.OperationalLogs)
                .HasForeignKey(d => d.UserManagerId);
        }
    }
}
