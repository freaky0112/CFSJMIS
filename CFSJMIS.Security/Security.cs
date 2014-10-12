using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

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


        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// 
        /// DES加密字符串
        /// 
        /// 待加密的字符串
        /// 加密密钥,要求为8位
        /// 加密成功返回加密后的字符串，失败返回源串
        public static string EncryptDES(string encryptString, string encryptKey) {
            try {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            } catch {
                return encryptString;
            }
        }
        /// 
        /// DES解密字符串
        /// 
        /// 待解密的字符串
        /// 解密密钥,要求为8位,和加密密钥相同
        /// 解密成功返回解密后的字符串，失败返源串
        public static string DecryptDES(string decryptString, string decryptKey) {
            try {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            } catch {
                return decryptString;
            }
        }
    }
    /// <summary>
    /// 对称加密
    /// </summary>
    public class SymmetricMethod {
        private static SymmetricAlgorithm mobjCryptoService;
        private static string Key;
        /// <summary>   
        /// 对称加密类的构造函数   
        /// </summary>   
        public  SymmetricMethod() {
            mobjCryptoService = new RijndaelManaged();
            Key = @"qQ)!ECmRkj~O[tF?NX:yyy7\|I$>P_|5k~K2e,:pyaBQ|nM[M7i:21[R*'izO6{z";
        }
        /// <summary>   
        /// 获得密钥   
        /// </summary>   
        /// <returns>密钥</returns>   
        private  byte[] GetLegalKey() {
            string sTemp = Key;
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>   
        /// 获得初始向量IV   
        /// </summary>   
        /// <returns>初试向量IV</returns>   
        private  byte[] GetLegalIV() {
            string sTemp = @"Jv<%B!K\t`V;Ero@h,{Gd_UAb&:PG>15@Yog=;3d>}&1<n9R|R?sc+\=}Cc^Od{P";
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>   
        /// 加密方法   
        /// </summary>   
        /// <param name="Source">待加密的串</param>   
        /// <returns>经过加密的串</returns>   
        public  string Encrypto(string Source) {
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            ms.Close();
            byte[] bytOut = ms.ToArray();
            return Convert.ToBase64String(bytOut);
        }
        /// <summary>   
        /// 解密方法   
        /// </summary>   
        /// <param name="Source">待解密的串</param>   
        /// <returns>经过解密的串</returns>   
        public  string Decrypto(string Source) {
            byte[] bytIn = Convert.FromBase64String(Source);
            MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}



