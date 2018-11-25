using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mints.BLayer.Exceptions
{
    public class DataException : Exception
    {
        public DataException(string message) : base(message) { }

        public DataException (string key, string message) : base(message)
        {
            DataAccessKey = key;
        }

        public string DataAccessKey { get; }
    }
}
