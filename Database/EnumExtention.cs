using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Database;

public static class EnumExtention
{
    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName();
    }
}