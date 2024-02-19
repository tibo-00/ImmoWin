using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data
{
    [Table("tblAppartementen")]
    public class Appartement : Woning
    {
        public int Verdieping { get; set; }
        public Appartement()
        {}
    }
}
