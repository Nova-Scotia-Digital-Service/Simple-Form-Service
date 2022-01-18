using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFormsService.Domain.Entities.Base;

namespace SimpleFormsService.Persistence.Configurations.Base
{
    public class LifespanBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : LifespanBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired();

            builder.HasKey(x => x.Id);
        }
    }
}