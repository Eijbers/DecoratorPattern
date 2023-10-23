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

            source =  new FileDataSource("C:\\Users\\AEijbers\\OneDrive - ilionx Group BV\\Documenten\\TestData\\DecoratorFileSource\\test3.txt");   
            source.WriteData(new List<string>() { "this is test data" });

            source = new CompressionDecorator(source);
            source.WriteData(new List<string>() { "this is also test data" });

            source = new EncryptionDecorator(source);
            source.WriteData(new List<string>() { "this is also test data again" });
        }
    }
}