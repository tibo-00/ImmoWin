using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data
{
    [Table("tblHuizen")]
    public class Huis : Woning
    {
        public int Huistype { get; set; }
    }
}
