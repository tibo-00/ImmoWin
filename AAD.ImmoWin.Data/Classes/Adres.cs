using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data
{
    [Table("tblAdressen")]
    public class Adres
    {
        public int Id {  get; set; }
        public String Straat { get; set; }
        public int Nummer { get; set; }
        public int Postnummer { get; set; }
        public String Gemeente { get; set; }

        public Adres()
        {}
    }
}
