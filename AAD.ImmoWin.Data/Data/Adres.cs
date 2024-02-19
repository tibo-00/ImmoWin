using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data.Classes
{
    public class Adres
    {
        public int Id {  get; set; }
        public String Straat { get; set; }
        public int Nummmer { get; set; }
        public int Postnummer { get; set; }
        public String Gemeente { get; set; }

        public Adres()
        {}
    }
}
