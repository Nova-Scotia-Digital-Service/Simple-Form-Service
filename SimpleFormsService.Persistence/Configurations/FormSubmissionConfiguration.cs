using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFormsService.Domain;
using SimpleFormsService.Persistence.Configurations.Base;

namespace SimpleFormsService.Persistence.Configurations
{
    public class FormSubmissionConfiguration : EntityBaseConfiguration<Domain.Entities.FormSubmission>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.FormSubmission> builder)
        {
            base.Configure(builder);

            builder.ToTable(builder.Metadata.ClrType.Name.CamelCaseUnderScorify());

            builder.HasOne(x => x.Template)
                .WithMany()
                .HasForeignKey(x => x.TemplateId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.TemplateId)
                .HasColumnName(builder.Property(x => x.TemplateId).Metadata.Name.CamelCaseUnderScorify())
                .IsRequired();

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