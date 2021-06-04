namespace DietPlanner.Shared.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class ToPasswordRepository
    {
        public static byte[] ByteConvert(string value)
        {
            UnicodeEncoding byteConverter = new();
            return byteConverter.GetBytes(value);
        }

        /// <summary>
        /// Crypt string to 8Byte 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] Byte8(string value)
        {
            char[] arrayChar = value.ToCharArray();
            byte[] arrayByte = new byte[arrayChar.Length];
            for (int i = 0; i < arrayByte.Length; i++)
                arrayByte[i] = Convert.ToByte(arrayChar[i]);
            return arrayByte;
        }
        /// <summary>
        /// Crypt string value to Md5
        /// </summary>
        /// <param name="incomestring"></param>
        /// <returns></returns>
        public static string Md5(string incomestring)
        {
            if (string.IsNullOrEmpty(incomestring))
                throw new (@"Şifrelenecek veri yok");
            else
            {
                MD5CryptoServiceProvider password = new();
                byte[] passwordByte = password.ComputeHash(Encoding.UTF8.GetBytes(incomestring));
                StringBuilder sb = new();
                foreach (byte b in passwordByte)
                    sb.Append(b.ToString("x2").ToLower());

                return sb.ToString();
            }
        }
        /// <summary>
        /// Crypt string value to  Sha1
        /// </summary>
        /// <param name="incomestring"></param>
        /// <returns></returns>
        public static string Sha1(string incomestring)
        {
            if (string.IsNullOrEmpty(incomestring))
                throw new (@"Şifrelenecek veri yok.");
            else
            {

                SHA1CryptoServiceProvider sifre = new();
                byte[] sifrebytes = sifre.ComputeHash(Encoding.UTF8.GetBytes(incomestring));
                StringBuilder sb = new ();
                foreach (byte b in sifrebytes)
                    sb.Append(b.ToString("x2").ToLower());

                return sb.ToString();
            }
        }
        /// <summary>
        /// Crypt string value to Sha256
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static string Sha256(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new(@"Şifrelenecek Veri Yok");
            else
            {
                SHA256Managed sifre = new ();
                byte[] arySifre = ByteConvert(login);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
        /// <summary>
        /// Crypt string value to Sha384
        /// </summary>
        /// <param name="strGiris"></param>
        /// <returns></returns>
        public static string Sha384(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
                throw new (@"Şifrelenecek veri bulunamadı.");
            else
            {
                SHA384Managed sifre = new ();
                byte[] arySifre = ByteConvert(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
        /// <summary>
        /// Crypt string value to Sha512
        /// </summary>
        /// <param name="strGiris"></param>
        /// <returns></returns>
        public static string Sha512(string strGiris)
        {
            if (string.IsNullOrEmpty(strGiris))
                throw new (@"Şifrelenecek veri yok.");
            else
            {
                SHA512Managed sifre = new ();
                byte[] arySifre = ByteConvert(strGiris);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }
        /// <summary>
        /// Crypting to:
        /// 1:Sha256
        /// 2:Sha384
        /// 3:Md5
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string PasswordCryptographyCombine(string pass)
        {
            return (Md5(Sha384(Sha256(pass))));
        }
    }
}
