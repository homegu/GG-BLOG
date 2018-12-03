using CoreWebApp.Infrastructure.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreWebApp.Infrastructure
{
    public static class MD5Helper
    {
        /// <summary>
        /// 将字符串生成MD5摘要
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <param name="mode">MD5字符串模式</param>
        /// <returns></returns>
        public static string MD5(string sourceStr, MD5Mode mode)
        {
            if (sourceStr == null)
            {
                sourceStr = string.Empty;
            }

            string s = MD5(sourceStr);

            switch (mode)
            {
                case MD5Mode.Mode16Lower:
                    s = s.Substring(8, 16).ToLower();
                    break;

                case MD5Mode.Mode32Lower:
                    s = s.ToLower();
                    break;

                case MD5Mode.Mode16Upper:
                    s = s.Substring(8, 16).ToUpper();
                    break;

                case MD5Mode.Mode32Upper:
                    s = s.ToUpper();
                    break;
            }

            return s;
        }

        /// <summary>
        /// 将字符串生成MD5摘要（32位小写）
        /// </summary>
        /// <param name="sourceStr">源字符串</param>
        /// <returns>将源字符串生成MD5后的字符串</returns>
        public static string MD5(string s)
        {
            using (var provider = System.Security.Cryptography.MD5.Create())
            {
                StringBuilder builder = new StringBuilder();

                foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(s)))
                    builder.Append(b.ToString("x2").ToLower());

                return builder.ToString();
            }
        }


        public static string MD5WithSalt(string src)
        {
            var text = src + SystemSecretKey.MD5_SALT;
            return MD5(text, MD5Mode.Mode32Upper);
        }

        public static string MD5WithSalt(string src,MD5Mode mode)
        {
            var text = src + SystemSecretKey.MD5_SALT;
            return MD5(text, mode);
        }
    }

    /// <summary>
    /// MD5模式
    /// </summary>
    public enum MD5Mode
    {
        /// <summary>
        /// 16位小写
        /// </summary>
        Mode16Lower,
        /// <summary>
        /// 32位小写
        /// </summary>
        Mode32Lower,
        /// <summary>
        /// 16位大写
        /// </summary>
        Mode16Upper,
        /// <summary>
        /// 32位大写
        /// </summary>
        Mode32Upper,
    }
}
