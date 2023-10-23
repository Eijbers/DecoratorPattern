using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DecoratorPattern.DataSource.Extensions;
using DecoratorPattern.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DecoratorPattern.Decorators
{
    internal class EncryptionDecorator : DataSourceDecorator
    {
        public EncryptionDecorator(IDataSource wrappee) : base(wrappee)
        {
        }
        public override byte[] ReadData()
        {
            string key = "thisisakeytousefortestencryption";

            byte[] data = this.wrappee.ReadData();
            byte[] iv = new byte[16];
            byte[] buffer = data;

            string returnString = "";

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
                                returnString += streamReader.ReadLine();
                            }                            
                        }
                    }
                }
            }
            return ExtendedSerializerExtensions.Serialize(returnString);
        }

        public override void WriteData(byte[] data)
        {
            string key = "thisisakeytousefortestencryption";

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
                           streamWriter.Write(ExtendedSerializerExtensions.Deserialize<string>(data));                        
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }
            this.wrappee.WriteData(array);
        }
    }
}
