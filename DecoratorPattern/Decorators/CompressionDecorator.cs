using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecoratorPattern.DataSource.Extensions;
using DecoratorPattern.Interfaces;

namespace DecoratorPattern.Decorators
{
    internal class CompressionDecorator : DataSourceDecorator
    {
        public CompressionDecorator(IDataSource wrappee) : base(wrappee)
        {
        }
        public List<string> ReadData()
        {
            byte[] bytes = ExtendedSerializerExtensions.Serialize(wrappee.ReadData());
            List<string> returnList = new List<string>();

            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }
                    foreach (var item in outputStream.ToArray())
                    {
                        //TO DO: Fix this. This is a weird conversion that will not result in a usable output.
                        returnList.Add(ExtendedSerializerExtensions.Deserialize<string>(outputStream.ToArray()));

                    }
                    return returnList;
                }
            }
        }

        public void WriteData(List<string> data)
        {
            byte[] byteData = ExtendedSerializerExtensions.Serialize(data);

            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(byteData, 0, byteData.Length);
                }
            }
        }
    }
}
