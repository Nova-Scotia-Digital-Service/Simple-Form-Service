using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleFormsService.Domain;
using SimpleFormsService.Persistence.Configurations.Base;

namespace SimpleFormsService.Persistence.Configurations.FormSubmission
{
    public class FormSubmissionConfiguration : EntityBaseConfiguration<Domain.Entities.FormSubmission.FormSubmission>
    {
        public override void Configure(EntityTypeBuilder<Domain.Entities.FormSubmission.FormSubmission> builder)
        {
            base.Configure(builder);
            
            builder.ToTable(builder.Metadata.ClrType.Name.CamelCaseUnderScorify());

            builder.Property(x => x.TemplateId)
                .HasColumnName(builder.Property(x => x.TemplateId).Metadata.Name.CamelCaseUnderScorify())
                .IsRequired();

            builder.Property(x => x.SubmissionData)
                .HasColumnName(builder.Property(x => x.SubmissionData).Metadata.Name.CamelCaseUnderScorify())
                .HasColumnType("jsonb")
                .IsRequired(false);
        }
    }
}