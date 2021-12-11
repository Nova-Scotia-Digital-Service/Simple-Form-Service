using System.Runtime.Serialization;

namespace SimpleFormsService.Domain.Entities.FormSubmission.Supporting
{
    public enum FormSubmissionStatus // todo use npgsql enum mapping strategy. if left this will map to an int by default
    {
        [EnumMember(Value = "INITIALIZED")]
        Initialized,
        [EnumMember(Value = "IN_PROGRESS")]
        InProgress,
        [EnumMember(Value = "SUBMITTED")]
        Submitted,
    }
}
