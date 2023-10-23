using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecoratorPattern.Interfaces;

namespace DecoratorPattern.Decorators
{
     abstract public class DataSourceDecorator : IDataSource
    {
        public IDataSource wrappee { get; set; }

        public DataSourceDecorator(IDataSource wrappee)
        {
            this.wrappee = wrappee;
        }
        abstract public byte[] ReadData();

        abstract public void WriteData(byte[] data);
    }
}
