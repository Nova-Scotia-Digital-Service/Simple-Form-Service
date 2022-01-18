using System.Text.Json.Serialization;

namespace SimpleFormsService.Domain.Entities.Base;

public interface IEntityBase
{
    Guid Id { get; }
   }

public abstract class EntityBase : IEntityBase
{
    protected EntityBase(Guid id)
    {
        if (id == default)
            throw new ArgumentNullException(nameof(id));

        Id = id;
    }

    [JsonIgnore] public Guid Id { get; set; }
}