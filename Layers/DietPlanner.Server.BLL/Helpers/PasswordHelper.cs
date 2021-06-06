using System;

namespace DietPlanner.Server.BLL.Helpers
{
    public static class PasswordHelper
    {
        public static string CreateRandomPassword()
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new();

            int size = random.Next(8, 32);
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
                chars[i] = validChars[random.Next(0, validChars.Length)];
            return new string(chars);
        }
    }
}
