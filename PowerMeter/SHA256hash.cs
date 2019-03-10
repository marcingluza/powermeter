using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PowerMeter
{
    public class SHA256hash
    {
        public static string getHash(string password)
        {
            SHA256 hash = new SHA256Managed();
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