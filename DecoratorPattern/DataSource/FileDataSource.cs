using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DecoratorPattern.DataSource.Extensions;
using DecoratorPattern.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DecoratorPattern.FileReader
{
    public class FileDataSource : IDataSource
    {
        public string FileName { get; set; }

        public FileDataSource(string FileName)
        {
            this.FileName = FileName;
        }
        public byte[] ReadData()
        {
            byte[] data;

            var sr = new StreamReader(this.FileName);
            var line = sr.ReadLine();
            while (line != null)
            {              
                line += sr.ReadLine();
            }
            return ExtendedSerializerExtensions.Serialize<string>(line); ;
        }

        public void WriteData(byte[] data)
        {
            //Open the File and append
            StreamWriter sw = new StreamWriter(this.FileName, true);

            var stringData = ExtendedSerializerExtensions.Deserialize<string>(data);
            sw.Write(stringData);
            

            //close the file
            sw.Close();
        }
    }
}
