using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateApp.Core.Data.Security
{
    static class CryptionExtension
    {
        public static T[] CombineArrays <T> (this T[] front, T[] back)
        {
            T[] newArray = new T[front.Length + front.Length];
            Array.Copy(front, newArray, front.Length);
            Array.Copy(back, 0, newArray, front.Length, back.Length);
            return newArray;
        }
        public static byte[] ToByteArray(this string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
        public static string FromByteArray(this byte[] text)
        {
            return Encoding.UTF8.GetString(text);
        }
        public static string ToBaseString(this byte[] arr)
        {
            return Convert.ToBase64String(arr);
        } 
        public static byte[] FromBaseString(this string str)
        {
            return Convert.FromBase64String(str);
        }
        public static bool EqualsByteArray(this byte[] arr1, byte[] arr2)
        {
            if (arr1.Length != arr2.Length)
            {
                return false;
            }
            for(int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i]!=arr2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
