using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WildFarm.Exceptions
{
    public class UneatableFoodException : Exception
    {
        public UneatableFoodException()
        {
        }

        public UneatableFoodException(string message) 
            : base(message)
        {
        }

    }
}
