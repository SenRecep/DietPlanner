using System;
using System.Linq;

namespace DietPlanner.Server.BLL.Helpers
{
    public static class PasswordHelper
    {
        const string UpperChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        const string LowerChars = "abcdefghijklmnopqrstuvwxyz";
        const string NumberChars = "0123456789";
        const string SpecialChars = "!@#$%^&*?_-";
        public static string CreateRandomPassword()
        {
            string validChars = $"{UpperChars}{LowerChars}{NumberChars}{SpecialChars}";
            Random random = new();
            int size = random.Next(8, 32);
            char[] chars = new char[size];
            while (true)
            {
                for (int i = 0; i < size; i++)
                    chars[i] = validChars[random.Next(0, validChars.Length)];
                if (
                    UpperChars.Any(x => chars.Contains(x)) &&
                    LowerChars.Any(x => chars.Contains(x)) &&
                    NumberChars.Any(x => chars.Contains(x)) &&
                    SpecialChars.Any(x => chars.Contains(x)))
                    break;
            }
            return new string(chars);
        }
    }
}
