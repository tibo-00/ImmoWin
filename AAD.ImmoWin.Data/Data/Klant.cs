using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data.Classes
{
    public class Klant
    {
        public int Id { get; set; }
        public String Voornaam { get; set; }
        public String Familienaam { get; set; }
        public List<Woning> Eigendommem { get; set; }

    }
}
