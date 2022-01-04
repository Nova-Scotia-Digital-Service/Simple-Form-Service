using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFormsService.Domain;
using SimpleFormsService.Persistence.Configurations.Base;

namespace SimpleFormsService.Persistence.Configurations
{
    public class FormTemplateConfiguration : EntityBaseConfiguration<Domain.Entities.FormTemplate>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.FormTemplate> builder)
        {
            base.Configure(builder);

            builder.ToTable(builder.Metadata.ClrType.Name.CamelCaseUnderScorify());
        }
    }
}