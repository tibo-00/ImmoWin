using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data
{
    [Table("tblWoningen")]
    public class Woning
    {
        public int Id { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? BouwDatum { get; set; }
        public Decimal? Waarde {  get; set; }
        public int AdresId { get; set; }
        [ForeignKey("AdresId")]
        public Adres Adres { get; set; }
        [ForeignKey("Eigenaar")]
        public int KlantId { get; set; }
        public Klant Eigenaar { get; set; }
        public Woning()
        {}
    }
}
