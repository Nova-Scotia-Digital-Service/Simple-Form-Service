using System.Runtime.Serialization;

namespace SimpleFormsService.Domain.Entities.Base;

public interface IEntityBase
{
    Guid Id { get; }
    DateTime CreateDate { get; set; }
    string CreateUser { get; set; }
    DateTime? UpdateDate { get; set; }
    string UpdateUser { get; set; }
}

public abstract class EntityBase : IEntityBase
{
    protected EntityBase(Guid id)
    {
        if (id == default)
            throw new ArgumentNullException(nameof(id));

        Id = id;

        PropertySetter.Invoke(this);
    }

    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string UpdateUser { get; set; }
}