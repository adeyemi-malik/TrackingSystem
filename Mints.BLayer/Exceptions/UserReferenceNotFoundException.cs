using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Exceptions
{
    public class UserReferenceNotFoundException: Exception
    {
        public UserReferenceNotFoundException(string message) : base(message) { }
    }
}
