using System.Linq;

namespace CatsConsumer
{
    public static class StringExtensions
    {
        public static string Capitalize(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            return input.First().ToString().ToUpper() + input.Substring(1).ToLower();
        }
    }
}
