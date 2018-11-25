using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Exceptions
{
    public class AnimalReferenceNotFoundException: Exception
    {
        public AnimalReferenceNotFoundException(string message) : base(message) { }

    }
}
