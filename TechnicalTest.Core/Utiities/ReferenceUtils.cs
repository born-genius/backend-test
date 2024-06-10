using System.Text.RegularExpressions;

namespace TechnicalTest.Core.Utiities
{
    public static class ReferenceUtils
    {
        public static string GenerateRandomDigits(int length)
        {
            string rand = Regex.Replace(Guid.NewGuid().ToString(), "[^1-9]", "");

            while (rand.Length < length)
            {
                rand += Regex.Replace(Guid.NewGuid().ToString(), "[^1-9]", "");
            }

            return rand.Substring(0, length);
        }
    }
}
