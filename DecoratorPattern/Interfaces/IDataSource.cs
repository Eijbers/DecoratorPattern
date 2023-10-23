using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecoratorPattern.Interfaces
{
    public interface IDataSource
    {
        public void WriteData(byte[] data);
        public byte[] ReadData();
    }
}
