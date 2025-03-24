using System.ComponentModel;

namespace Volvo.Fleet.Extentions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescriptionValue(this Enum enumValue)
        {
            var type = enumValue.GetType();
            var info = type.GetField(enumValue.ToString());
            var da = (DescriptionAttribute[])(info.GetCustomAttributes(typeof(DescriptionAttribute), false));

            if (da.Length > 0)
                return da[0].Description;
            else
                return string.Empty;
        }
    }
}