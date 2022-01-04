using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFormsService.Domain.Entities.Base
{
    public interface ILifespanBase
    {
        DateTime? EffectiveDate { get; set; }
        DateTime? ExpiryDate { get; set; }
    }

    public abstract class LifespanBase : EntityBase, ILifespanBase
    {
        #region Constructors

        protected LifespanBase(Guid guidId) : base(guidId)
        {}

        #endregion

        #region Properties

        [NotMapped] public DateTime? EffectiveDate { get; set; } = DateTime.Now;
        [NotMapped] public DateTime? ExpiryDate { get; set; }

        #endregion
    }
}