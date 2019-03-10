using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PowerMeter
{
    public class MD5hash
    {
        public static string getHash(string password)
        {
            MD5CryptoServiceProvider hash = new MD5CryptoServiceProvider();
            {
                byte[] arr = System.Text.Encoding.UTF8.GetBytes(password);
                arr = hash.ComputeHash(arr);
                StringBuilder stringBuilder = new StringBuilder();
                foreach(byte b in arr)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }
}