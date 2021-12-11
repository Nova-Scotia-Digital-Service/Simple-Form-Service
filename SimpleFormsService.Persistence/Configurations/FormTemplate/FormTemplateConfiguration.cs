using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFormsService.Domain;
using SimpleFormsService.Persistence.Configurations.Base;

namespace SimpleFormsService.Persistence.Configurations.FormTemplate
{
    public class FormTemplateConfiguration : EntityBaseConfiguration<Domain.Entities.FormTemplate.FormTemplate>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.FormTemplate.FormTemplate> builder)
        {
            base.Configure(builder);
            
            builder.ToTable(builder.Metadata.ClrType.Name.CamelCaseUnderScorify());
        }
    }
}