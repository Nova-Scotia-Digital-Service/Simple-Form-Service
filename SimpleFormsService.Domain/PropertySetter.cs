using SimpleFormsService.Domain.Entities.Base;

namespace SimpleFormsService.Domain
{
    public static class PropertySetter
    {
        public static void Invoke<T>(T entity) where T : class
        {
            if (!(entity is IEntityBase))
                return;

            if (entity is IEntityBase)
            {
                if (string.IsNullOrEmpty((entity as IEntityBase).CreateUser))
                {
                    (entity as IEntityBase).CreateUser = Environment.UserName;
                }
                else if (string.IsNullOrEmpty((entity as IEntityBase).UpdateUser))
                {
                    (entity as IEntityBase).UpdateUser = Environment.UserName;
                }

                if ((entity as IEntityBase).CreateDate == default)
                {
                    (entity as IEntityBase).CreateDate = DateTime.Now.ToUniversalTime();
                }
                else if ((entity as IEntityBase).UpdateDate == default)
                {
                    (entity as IEntityBase).UpdateDate = DateTime.Now.ToUniversalTime();
                }

                foreach (var item in (entity as IEntityBase).GetEnumerableChildren())
                {
                    foreach (var newItem in item.Value ?? new List<IEntityBase>())
                    {
                        if (!(newItem is IEntityBase))
                            continue;

                        Invoke(newItem as IEntityBase);
                    }
                }

                foreach (var item in (entity as IEntityBase).GetChildren())
                {
                    Invoke(item.Value);
                }
            }
        }
    }
}