using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SimpleFormsService.Domain.Entities.Supporting.JSON;

[NotMapped]
public class FormItem
{
    public FormItem(string name, string value)
    {
        Name = name;
        Value = value;
    }

    [JsonPropertyOrder(1)]
    public string Name { get; set; }
    [JsonPropertyOrder(2)]
    public string Value { get; set; }
}