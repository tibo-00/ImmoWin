using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Exceptions
{
    public class KlantException : Exception
    {
        public KlantException(String message)
            :base(message)
        {}
    }

    public class FamilienaamLeeg_KlantException : KlantException
    {
        public FamilienaamLeeg_KlantException() 
            : base("Klant familienaam mag niet leeg zijn")
        {
        }
    }

    public class VoornaamLeeg_KlantException : KlantException
    {
        public VoornaamLeeg_KlantException()
            : base("Klant voornaam mag niet leeg zijn")
        {
        }
    }
}
