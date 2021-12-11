using System.Runtime.Serialization;
using SimpleFormsService.Domain.Entities.Base;

namespace SimpleFormsService.Domain;

public static class Extensions
{
    #region IEntityBase Extensions

    public static Dictionary<string, System.Collections.IEnumerable> GetEnumerableChildren(this IEntityBase input)
    {
        var response = new Dictionary<string, System.Collections.IEnumerable>();

        foreach (var property in input.GetType().GetProperties())
            if (property.PropertyType.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)) &&
                property.PropertyType.GetGenericArguments().Any())
                response.Add(property.Name, property.GetValue(input, null) as System.Collections.IEnumerable);

        return response;
    }

    public static Dictionary<string, IEntityBase> GetChildren(this IEntityBase input)
    {
        var response = new Dictionary<string, IEntityBase>();

        foreach (var property in input.GetType().GetProperties())
            if (property.PropertyType.GetInterfaces().Contains(typeof(IEntityBase)))
                response.Add(property.Name, property.GetValue(input, null) as IEntityBase);

        return response;
    }

    public static void NullifyIds(this IEntityBase input)
    {
        foreach (var property in input.GetType().GetProperties())
        {
            if (property.Name == nameof(IEntityBase.Id))
                property.SetValue(input, default(uint));

            if (property.Name == nameof(IEntityBase.Id))
                property.SetValue(input, Guid.NewGuid());

            if (property.PropertyType.GetInterfaces().Contains(typeof(IEntityBase))) // child objects 
                if (property.GetValue(input, null) != null)
                    ((IEntityBase)property.GetValue(input, null)).NullifyIds();

            if (property.PropertyType.GetInterfaces().Contains(typeof(System.Collections.IEnumerable)) &&
                property.PropertyType.GetGenericArguments().Any()) // child collections
                if (property.GetValue(input, null) is System.Collections.IEnumerable enumerable)
                    foreach (var item in enumerable)
                        ((IEntityBase)item).NullifyIds();
        }
    }

    #endregion

    #region Enum Extensions

    public static string GetEnumMemberAttributeValueFromEnumValue(this Enum enumValue)
    {
        var attribute = enumValue.GetType()
            .GetField(enumValue.ToString())
            .GetCustomAttributes(typeof(EnumMemberAttribute), false)
            .SingleOrDefault() as EnumMemberAttribute;
        return attribute == null ? enumValue.ToString() : attribute.Value;
    }

    public static T GetEnumValueFromEnumMemberAttributeValue<T>(this string enumMemberAttributeValue)
    {
        var type = typeof(T);

        if (!type.IsEnum)
            throw new ArgumentException();

        var fields = type.GetFields();
        var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(EnumMemberAttribute), false),
            (f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((EnumMemberAttribute)a.Att).Value == enumMemberAttributeValue);
        return field == null ? default : (T)field.Field.GetRawConstantValue();
    }

    public static bool DoesEnumMemberAttributeValueExistInEnum<T>(this string enumMemberAttributeValue)
    {
        var type = typeof(T);

        if (!type.IsEnum)
            throw new ArgumentException();

        var fields = type.GetFields();
        var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(EnumMemberAttribute), false),
            (f, a) => new { Field = f, Att = a }).SingleOrDefault(a => ((EnumMemberAttribute)a.Att).Value == enumMemberAttributeValue);
        return field != null;
    }
    #endregion

    #region String Extensions

    public static string CamelCaseUnderScorify(this string input)
    {
        var underscored = string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString()));
        return underscored;
    }

    #endregion
}