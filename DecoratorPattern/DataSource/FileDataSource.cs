using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DecoratorPattern.DataSource.Extensions;
using DecoratorPattern.Interfaces;

namespace DecoratorPattern.FileReader
{
    public class FileDataSource : IDataSource
    {
        public string FileName { get; set; }

        public FileDataSource(string FileName)
        {
            this.FileName = FileName;
        }
        public List<string> ReadData()
        {
            List<string> list = new List<string>();

            var sr = new StreamReader(this.FileName);
            var line = sr.ReadLine();
            while (line != null)
            {
                list.Add(line);
                line = sr.ReadLine();
            }
            return list;
        }

        public void WriteData(List<string> data)
        {
            //Open the File and append in ascii format
            StreamWriter sw = new StreamWriter(this.FileName, true, Encoding.ASCII);
           
            for (int x = 0; x < data.Count; x++)
            {
                sw.Write(data[x]);
            }

            //close the file
            sw.Close();
        }
    }
}
