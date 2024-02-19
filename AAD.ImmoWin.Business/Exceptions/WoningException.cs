using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Exceptions
{
    public class WoningException : Exception
    {
        public WoningException(String message)
            : base(message)
        {}
    }

    public class WaardeTeKlein_WoningException : WoningException
    {
        public WaardeTeKlein_WoningException() 
            : base("Waarde mag niet kleiner zijn dan 0.")
        {
        }
    }

    public class BouwdatumTeGroot_WoningException : WoningException
    {
        public BouwdatumTeGroot_WoningException()
            : base("Bouwdatum is verplicht en mag niet in de toekomst liggen.")
        {
        }
    }

    public class EigenaarLeeg_WoningException : WoningException
    {
        public EigenaarLeeg_WoningException()
            : base("Een woning moet een eigenaar hebben!")
        {
        }
    }
}
