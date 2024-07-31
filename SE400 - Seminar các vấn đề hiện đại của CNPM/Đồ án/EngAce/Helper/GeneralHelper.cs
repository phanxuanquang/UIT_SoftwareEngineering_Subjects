using System.ComponentModel;
using System.Reflection;

namespace Helper
{
    public static class GeneralHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static ushort GetTotalWords(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            char[] delimiters = { ' ', '\r', '\n', '\t', '.', ',', ';', ':', '!', '?' };

            string[] words = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            return (ushort)words.Length;
        }

        public static bool IsEnglish(string input)
        {
            char[] englishAlphabetAndPunctuation = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.,!?;:'\"()[]{}$%&*+-/".ToCharArray();

            return input.All(c => englishAlphabetAndPunctuation.Contains(c) || char.IsWhiteSpace(c) || char.IsDigit(c));
        }
    }
}
