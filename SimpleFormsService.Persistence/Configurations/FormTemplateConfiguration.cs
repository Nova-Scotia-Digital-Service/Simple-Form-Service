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

            #region JSON 

            // POCO Mapping https://www.npgsql.org/efcore/mapping/json.html?tabs=fluent-api%2Cpoco#tabpanel_4MQ50Ciht1_fluent-api

            builder.Property(x => x.Data)
                .HasColumnName(builder.Property(x => x.Data).Metadata.Name.CamelCaseUnderScorify())
                .HasColumnType("jsonb")
                .IsRequired(false);

            #endregion
        }
    }
}