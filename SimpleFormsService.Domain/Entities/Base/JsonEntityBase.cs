using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SimpleFormsService.Domain.Entities.Base;

public interface IJsonEntityBase
{
    string CreateDate { get; set; }
    string CreateUser { get; set; }
    string UpdateDate { get; set; }
    string UpdateUser { get; set; }
}

[NotMapped]
public abstract class JsonEntityBase : IJsonEntityBase
{
    protected JsonEntityBase(string createDate, string createUser, string updateDate, string updateUser)
    {
        CreateDate = createDate;
        CreateUser = createUser;
        UpdateDate = updateDate;
        UpdateUser = updateUser;
    }

    [JsonPropertyOrder(9996)]
    public string CreateDate { get; set; }
    [JsonPropertyOrder(9997)]
    public string CreateUser { get; set; }
    [JsonPropertyOrder(9998)] 
    public string UpdateDate { get; set; }
    [JsonPropertyOrder(9999)] 
    public string UpdateUser { get; set; } 
}