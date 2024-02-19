using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data
{
    [Table("tblKlanten")]
    public class Klant
    {
        public int Id { get; set; }
        public String Voornaam { get; set; }
        public String Familienaam { get; set; }

        public Klant()
        {
        }
    }
}
