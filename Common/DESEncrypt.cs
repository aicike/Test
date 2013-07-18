using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Poco;

namespace Common
{
  public sealed  class DESEncrypt
    {
        /// <summary>     
        /// 对字符串进行DES加密  
        /// </summary>     
        /// <param name="sourcestring">待加密的字符串</param>  
        /// <param name="key">必须长度等于8的字符串</param>
        /// <param name="iv">必须长度大于等于8的字符串</param>
        /// <returns>加密后的BASE64编码的字符串</returns>     
        public static string Encrypt(string src)
        {
            string key = SystemConst.Business.key;
            string iv = SystemConst.Business.iv;
            byte[] byteKey = Encoding.Default.GetBytes(key);
            byte[] byteIv = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {

                try
                {
                    byte[] data = Encoding.Default.GetBytes(src);
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byteKey, byteIv), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>  
        /// 对DES加密后的字符串进行解密     
        /// </summary>     
        /// <param name="encryptedString">待解密的字符串</param>     
        /// <param name="key">同加密时所采用的key</param>
        /// <param name="iv">同加密时所采用的iv</param>
        /// <returns>解密后的字符串</returns>     
        public static string Decrypt(string src)
        {
            string key = SystemConst.Business.key;
            string iv = SystemConst.Business.iv;
            byte[] byteKey = Encoding.Default.GetBytes(key);
            byte[] byteIv = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    byte[] data = Convert.FromBase64String(src);
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byteKey, byteIv), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>  
        /// 对文件内容进行DES加密     
        /// </summary>     
        /// <param name="sourceFile">待加密的文件绝对路径</param>     
        /// <param name="destFile">加密后的文件保存的绝对路径</param>   
        /// <param name="key">必须长度等于8的字符串</param>
        /// <param name="iv">必须长度大于等于8的字符串</param>
        public static void EncryptFile(string sourceFile, string destFile, string key, string iv)
        {
            if (!File.Exists(sourceFile))
                throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);

            byte[] byteKey = Encoding.Default.GetBytes(key);
            byte[] byteIV = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] byteFile = File.ReadAllBytes(sourceFile);

            using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(byteKey, byteIV), CryptoStreamMode.Write))
                    {
                        cs.Write(byteFile, 0, byteFile.Length);
                        cs.FlushFinalBlock();
                    }
                }
                catch
                {
                    throw;
                }

                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary>     
        /// Encrypts the file.     
        /// 对文件内容进行DES加密，加密后覆盖掉原来的文件     
        /// </summary>     
        /// <param name="sourceFile">The source file.待加密的文件的绝对路径</param>     
        /// <param name="key">必须长度等于8的字符串</param>
        /// <param name="iv">必须长度大于等于8的字符串</param>
        public static void EncryptFile(string sourceFile, string key, string iv)
        {
            EncryptFile(sourceFile, sourceFile, key, iv);
        }

        /// <summary> 
        /// 对文件内容进行DES解密     
        /// </summary>     
        /// <param name="sourceFile">待解密的文件绝对路径</param>     
        /// <param name="destFile">解密后的文件保存的绝对路径</param>
        /// <param name="key">同加密时所采用的key</param>
        /// <param name="iv">同加密时所采用的iv</param>
        public static void DecryptFile(string sourceFile, string destFile, string key, string iv)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);
            byte[] byteKey = Encoding.Default.GetBytes(key);
            byte[] byteIV = Encoding.Default.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] byteFile = File.ReadAllBytes(sourceFile);

            using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    using (CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(byteKey, byteIV), CryptoStreamMode.Write))
                    {
                        cs.Write(byteFile, 0, byteFile.Length);
                        cs.FlushFinalBlock();
                    }
                }
                catch
                {
                    throw;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        /// <summary> 
        /// 对文件内容进行DES解密，加密后覆盖掉原来的文件     
        /// </summary>     
        /// <param name="sourceFile">待解密的文件的绝对路径</param>     
        /// <param name="key">同加密时所采用的key</param>
        /// <param name="iv">同加密时所采用的iv</param>
        public static void DecryptFile(string sourceFile, string key, string iv)
        {
            DecryptFile(sourceFile, sourceFile, key, iv);
        }
    }
}
