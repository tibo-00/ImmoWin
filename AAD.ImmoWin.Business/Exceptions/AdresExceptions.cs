using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Exceptions
{
    public class AdresException : Exception
    {
        public AdresException(String message)
            : base(message) 
        { }
    }

    public class StraatLeeg_AdresException : AdresException
    {
        public StraatLeeg_AdresException() 
            : base("Straat mag niet leeg zijn.")
        {}
    }

    public class GemeenteLeeg_AdresException : AdresException
    {
        public GemeenteLeeg_AdresException()
            : base("Gemeente mag niet leeg zijn.")
        { }
    }

    public class NummerTeKlein_AdresException : AdresException
    {
        public NummerTeKlein_AdresException()
            : base("Nummer moet groter zijn dan 0.")
        { }
    }

    public class PostnummerTeKlein_AdresException : AdresException
    {
        public PostnummerTeKlein_AdresException()
            : base("Postnummer moet groter zijn dan 0.")
        { }
    }
}
