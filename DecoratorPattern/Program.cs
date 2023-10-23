using DecoratorPattern.DataSource.Extensions;
using DecoratorPattern.Decorators;
using DecoratorPattern.FileReader;
using DecoratorPattern.Interfaces;

namespace DecoratorPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDataSource source;

            source =  new FileDataSource("C:\\Users\\AEijbers\\OneDrive - ilionx Group BV\\Documenten\\TestData\\DecoratorFileSource\\testPattern.txt");           
            source.WriteData(ExtendedSerializerExtensions.Serialize("this is test data"));

            source = new EncryptionDecorator(source);
            source.WriteData(ExtendedSerializerExtensions.Serialize("this is also test data too"));

            source = new CompressionDecorator(source);
            source.WriteData(ExtendedSerializerExtensions.Serialize("this is also test data"));

           
        }
    }
}