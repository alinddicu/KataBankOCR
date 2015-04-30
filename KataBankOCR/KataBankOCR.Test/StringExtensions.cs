namespace KataBankOCR.Test
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    public static class StringExtensions
    {
        public static string ReplaceCharAtIndex(this string value, int index, string replacement)
        {
            if (replacement.Count()  != 1)
            {
                throw new InvalidDataException("String with only 1 characters allowed");
            }

            var sb = new StringBuilder(value);
            sb[index] = replacement[0];

            return sb.ToString();
        }

        public static string[] ToStringArray(this string value)
        {
            return value.ToCharArray().Select(c => c.ToString(CultureInfo.InvariantCulture)).ToArray();
        }
    }
}