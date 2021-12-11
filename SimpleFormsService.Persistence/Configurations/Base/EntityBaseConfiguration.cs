using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFormsService.Domain;
using SimpleFormsService.Domain.Entities.Base;

namespace SimpleFormsService.Persistence.Configurations.Base
{
    public class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.CreateDate).HasDefaultValueSql("now()")
                .HasColumnName(builder.Property(x => x.CreateDate).Metadata.Name.CamelCaseUnderScorify())
                .IsRequired();

            builder.Property(x => x.CreateUser)
                .HasColumnName(builder.Property(x => x.CreateUser).Metadata.Name.CamelCaseUnderScorify())
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.UpdateDate).HasDefaultValueSql("now()")
                .HasColumnName(builder.Property(x => x.UpdateDate).Metadata.Name.CamelCaseUnderScorify())
                .IsRequired()
                .IsConcurrencyToken();

            builder.Property(x => x.UpdateUser)
                .HasColumnName(builder.Property(x => x.UpdateUser).Metadata.Name.CamelCaseUnderScorify())
                .IsRequired()
                .HasMaxLength(50);

            builder.HasKey(x => x.Id);
        }
    }
}