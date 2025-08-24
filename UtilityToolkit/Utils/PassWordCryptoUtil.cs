using NPOI.HSSF.Record.Chart;
using System.Security.Cryptography;
using System.Text;

namespace UtilityToolkit.Utils
{
    /// <summary>
    /// 密码处理
    /// </summary>
    public static class PasswordCryptoUtil
    {
        /// <summary>
        /// 公钥加密
        /// </summary>
        public static readonly string _publicKey = "";
           
        /// <summary>
        /// 私钥解密
        /// </summary>
        public static readonly string _privateKey= "";


        /// <summary>
        /// 解密处理
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            using (var rsa = RSA.Create())
            {
                rsa.ImportFromPem(_privateKey);
                var encryptedData = Convert.FromBase64String(cipherText);
                var decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(decryptedData);
            }
        }
    }
}


