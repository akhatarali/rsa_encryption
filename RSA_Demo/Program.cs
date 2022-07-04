using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RSA_Demo
{
    internal class Program
    {
        // Global Variables
        private static string _privateKey;
        private static string _publicKey;

        static void Main(string[] args)
        {
            var rsa = new RSACryptoServiceProvider();
            _privateKey = rsa.ToXmlString(true); // Generate Public Key
            _publicKey = rsa.ToXmlString(false); // Generate Private Key

            var simpleText = "Welcome to Github";
            Console.WriteLine("RSA // Text to encrypt: " + simpleText);

            var encryptedText = Encrypt(simpleText);
            Console.WriteLine("RSA // Encrypted Text: " + encryptedText);
            
            var decryptedText = Decrypt(encryptedText);
            Console.WriteLine("RSA // Decrypted Text: " + decryptedText);
        }

        public static string Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(_publicKey);

            var encoder = new UnicodeEncoding();
            var dataToEncrypt = encoder.GetBytes(data);
            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();
            var length = encryptedByteArray.Count();
            var item = 0;
            var sbEncryptText = new StringBuilder();

            foreach (var encryptedByte in encryptedByteArray)
            {
                item++;
                sbEncryptText.Append(encryptedByte);

                if (item < length)
                    sbEncryptText.Append(",");
            }
            return sbEncryptText.ToString();
        }

        public static string Decrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();
            var dataArray = data.Split(new char[] { ',' });

            byte[] dataByte = new byte[dataArray.Length];
            
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataByte[i] = Convert.ToByte(dataArray[i]);
            }

            rsa.FromXmlString(_privateKey);

            var decryptedByte = rsa.Decrypt(dataByte, false);
            var encoder = new UnicodeEncoding();
            return encoder.GetString(decryptedByte);
        }
    }
}