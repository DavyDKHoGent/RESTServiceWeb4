using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTServiceWeb4.Models
{
    public class CountryException : Exception
    {
        public CountryException(string message) : base(message)
        {

        }
    }
}
