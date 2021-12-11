using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.FormSubmission.Supporting.JSON;

[NotMapped]
public class FormItem
{
    public FormItem(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public string Value { get; set; }
}