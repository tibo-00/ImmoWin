using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Exceptions
{
    public class AppartementException : Exception
    {
        public AppartementException(String message)
            : base(message)
        { }
    }

    public class VerdiepingLeeg_AppartementException : AppartementException
    {
        public VerdiepingLeeg_AppartementException()
            : base("Een appartement moet een verdieping hebben die groter of gelijk is aan nul!")
        {
        }
    }
}
