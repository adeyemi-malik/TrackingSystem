using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Exceptions
{
    public class RoleReferenceNotFoundException: Exception
    {
        public RoleReferenceNotFoundException(string message) : base(message) { }
    }
}
