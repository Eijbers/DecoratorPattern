using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DecoratorPattern.DataSource.Extensions;
using DecoratorPattern.Interfaces;

namespace DecoratorPattern.Decorators
{
    internal class EncryptionDecorator : DataSourceDecorator
    {
        public EncryptionDecorator(IDataSource wrappee) : base(wrappee)
        {
        }
        public List<string> ReadData()
        {
            string key = "thisisakey";
            List<string> returnList = new List<string>();

            List<string> data = this.wrappee.ReadData();
            byte[] iv = new byte[16];
            byte[] buffer = ExtendedSerializerExtensions.Serialize<List<string>>(data);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            while(!streamReader.EndOfStream) 
                            {
                                returnList.Add(streamReader.ReadLine());
                            }                            
                        }
                    }
                }
            }
            return returnList;
        }

        public void WriteData(List<string> data)
        {
            string key = "thisisakey";

            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            foreach (string s in data) { streamWriter.Write(s); }                            
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
        }
    }
}
