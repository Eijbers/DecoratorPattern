using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecoratorPattern.Interfaces;

namespace DecoratorPattern.Decorators
{
    internal class DataSourceDecorator : IDataSource
    {
        public IDataSource wrappee { get; set; }

        public DataSourceDecorator(IDataSource wrappee)
        {
            this.wrappee = wrappee;
        }
        public List<string> ReadData()
        {
            return wrappee.ReadData();
        }

        public void WriteData(List<string> data)
        {
            wrappee.WriteData(data);
        }
    }
}
