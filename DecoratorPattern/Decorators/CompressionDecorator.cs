﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecoratorPattern.DataSource.Extensions;
using DecoratorPattern.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DecoratorPattern.Decorators
{
    internal class CompressionDecorator : DataSourceDecorator
    {
        public CompressionDecorator(IDataSource wrappee) : base(wrappee)
        {
        }
        public override byte[] ReadData()
        {
            byte[] bytes = ExtendedSerializerExtensions.Serialize(wrappee.ReadData());

            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }       
                    return outputStream.ToArray();
                }
            }
        }

        public override void WriteData(byte[] data)
        {
            byte[] byteData = ExtendedSerializerExtensions.Serialize(data);
            byte[] array;

            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(byteData, 0, byteData.Length);                   
                }
                array = memoryStream.ToArray();              
            }
            this.wrappee.WriteData(array);
        }
    }
}
