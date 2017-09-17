using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace CCM.Business.Utilities
{
    public static class EnumUtilities
    {

        public static Dictionary<string, int> ListTheEnum<T>() where T : struct, IConvertible
        {
            if (!typeof(T).GetTypeInfo().IsEnum)
                throw new ArgumentException("enumType should describe enum");

            Dictionary<string, int> enumDictionary = new Dictionary<string, int>();

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                string text = GetEnumDescription(item as Enum);
                enumDictionary.Add(text, (int)item);
            }

            return enumDictionary;
        }

        public static string GetEnumDescription<T>(string enumString) where T : struct, IConvertible
        {
            T e = EnumFromString<T>(enumString);
            return GetEnumDescription(e as Enum);
        }

        public static string GetEnumDescription<T>(int enumInt) where T : struct, IConvertible
        {
            T e = EnumFromInt<T>(enumInt);
            return GetEnumDescription(e as Enum);
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static T EnumFromInt<T>(int enumInt) where T : struct, IConvertible
        {
            return (T)Enum.ToObject(typeof(T), enumInt);           
        }

        public static T EnumFromString<T>(string enumString) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), enumString, true);
        }       

    }
}
