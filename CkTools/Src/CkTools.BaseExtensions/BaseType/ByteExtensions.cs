using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class ByteExtensions
    {
        /// <summary>
        /// 将byte[] 转换为<see cref="string"/>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToStr(this byte[] source, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8;
            return source.BaseConvertOrDefalut(encoding.GetString, string.Empty);
        }

        public static string[] BytesToHex(this byte[] bytes)
        {
            if (bytes.IsNullOrEmpty())
            {
                return Array.Empty<string>();
            }

            List<string> temp = new List<string>();
            foreach (byte item in bytes)
            {
                temp.Add(item.ToString("X2"));
            }
            return temp.ToArray();
        }
    }
}