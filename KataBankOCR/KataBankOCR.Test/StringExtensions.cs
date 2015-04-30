namespace KataBankOCR.Test
{
    using System.Linq;
    using System.Text;

    public static class StringExtensions
    {
        public static string ReplaceCharAtIndex(this string value, int index, char replacement)
        {
            var sb = new StringBuilder(value);
            sb[index] = replacement;

            return sb.ToString();
        }

        public static string ReplaceCharAtIndex(this string value, int index, string replacement)
        {
            return value.ReplaceCharAtIndex(index, replacement[0]);
        }

        public static string[] ToStringArray(this string value)
        {
            return value.ToCharArray().Select(c => c.ToString()).ToArray();
        }
    }
}