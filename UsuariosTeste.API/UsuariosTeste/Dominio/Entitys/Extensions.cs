using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace UsuariosTeste.Dominio.Entitys
{
    public static class Extensions
    {
        public static string GetEnumID<T>(this T enumerationValue) where T : IConvertible
        {
            var type = enumerationValue.GetType();
            if (type.IsEnum)
            {
                var memberInfo = type.GetMember(enumerationValue.ToString());
                if (memberInfo.Length > 0)
                {
                    var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs.Length > 0)
                    {
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
                return enumerationValue.ToString();
            }
            return "";
        }
        public static string GetEnumDescription<T>(this T enumerationValue) where T : IConvertible
        {
            var type = enumerationValue.GetType();
            if (type.IsEnum)
            {
                var memberInfo = type.GetMember(enumerationValue.ToString());
                if (memberInfo.Length > 0)
                {
                    var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                    if (attrs.Length > 0)
                    {
                        return ((DescriptionAttribute)attrs[0]).Description;
                    }
                }
                return enumerationValue.ToString();
            }
            return "";
        }
        public static T ConvertTo<T>(this decimal value) where T : struct => ConvertTo<T>((object)value);
        public static T? ConvertTo<T>(this decimal? value) where T : struct => ToNullable<T>((object)value);
        public static T? ConvertTo<T>(this int? value) where T : struct => ToNullable<T>((object)value);
        private static T ConvertTo<T>(object value) where T : struct => (T)Convert.ChangeType(value, typeof(T));
        public static T GetEnumValue<T>(this string value)
        {
            var type = typeof(T);
            if (!type.GetTypeInfo().IsEnum)
                throw new ArgumentException();
            var field = type.GetFields().SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false), (f, a) => new { Field = f, Att = a })
                                        .Where(a => ((DescriptionAttribute)a.Att).Description == value).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }
        private static T? ToNullable<T>(object value) where T : struct
        {
            if (typeof(T).IsEnum)
            {
                if (Nullable.GetUnderlyingType(value.GetType()) != (Type)null)
                {
                    return (T?)Convert.ChangeType(value, typeof(int?));
                }
                else
                {
                    return new T?((T)Convert.ChangeType(value, typeof(int)));
                }
            }
            else
            {
                if (!(Nullable.GetUnderlyingType(value.GetType()) != (Type)null))
                {
                    return (T?)Convert.ChangeType(value, typeof(T));
                }
                else
                {
                    return (T?)Convert.ChangeType(value, typeof(T?));
                }
            }
        }
    }
}


