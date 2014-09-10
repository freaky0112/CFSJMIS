using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CFSJMIS.Security {
    public abstract class Security {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string getMD5(string input) {
            MD5 md5 = MD5.Create();//建立一個MD5

            byte[] source = Encoding.Default.GetBytes(input);//將字串轉為Byte[]

            byte[] crypto = md5.ComputeHash(source);//進行MD5加密

            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串

            return result;
        }
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string getSHA1(string input) {
            SHA1 sha1 = new SHA1CryptoServiceProvider();//建立一個SHA1
            byte[] source = Encoding.Default.GetBytes(input);//將字串轉為Byte[]
            byte[] crypto = sha1.ComputeHash(source);//進行SHA1加密
            string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            return result;
        }

    }


}
