using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data.Classes
{
    public class Woning
    {
        public int Id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? BouwDatum { get; set; }
        public Decimal? Waarde {  get; set; }

        public Woning()
        {}
    }
}
