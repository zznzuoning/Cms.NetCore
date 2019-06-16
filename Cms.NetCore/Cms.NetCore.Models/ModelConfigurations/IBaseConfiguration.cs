using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.NetCore.Models.ModelConfigurations
{
    public abstract class IBaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public abstract void Configures(EntityTypeBuilder<T> builder);
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Sid).HasColumnName("Sid").ValueGeneratedOnAdd();
            builder.Property(d => d.CreateUserId).HasColumnName("CreateUserId");
            builder.Property(d => d.CreateTime).HasColumnName("CreateTime").HasDefaultValueSql("getdate()").ValueGeneratedOnAdd();
            builder.Property(d => d.UpdateUserId).HasColumnName("UpdateUserId");
            builder.Property(d => d.UpdateTime).HasColumnName("UpdateTime").HasDefaultValueSql("getdate()").ValueGeneratedOnAddOrUpdate();
            ////外键
            builder.HasOne(d => d.CreateUser).WithOne().OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UpdateUser).WithOne().OnDelete(DeleteBehavior.Restrict );
            Configures(builder);
        }
      
    }
}
