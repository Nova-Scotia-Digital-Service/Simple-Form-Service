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

        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        #endregion
    }
}