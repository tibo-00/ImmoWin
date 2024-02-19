using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Exceptions
{
    public class HuisException : Exception
    {
        public HuisException(String message)
            : base(message)
        { }
    }

    public class HuisTypeLeeg_HuisException : HuisException
    {
        public HuisTypeLeeg_HuisException()
            : base("Een huis moet een type hebben!")
        {
        }
    }
}
