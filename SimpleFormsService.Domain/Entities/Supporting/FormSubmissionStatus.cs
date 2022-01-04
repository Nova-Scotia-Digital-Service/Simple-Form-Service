using System.Runtime.Serialization;

namespace SimpleFormsService.Domain.Entities.Supporting
{
    public enum FormSubmissionStatus 
    {
        [EnumMember(Value = "INITIALIZED")]
        Initialized,
        [EnumMember(Value = "IN_PROGRESS")]
        InProgress,
        [EnumMember(Value = "SUBMITTED")]
        Submitted,
    }
}
