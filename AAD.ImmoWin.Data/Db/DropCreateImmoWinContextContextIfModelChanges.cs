using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data
{
    public class DropCreateImmoWinContextContextIfModelChanges : DropCreateDatabaseIfModelChanges<ImmoWinContext>
    {
        protected override void Seed(ImmoWinContext context)
        {
            Klant k;
            Woning w;

            k = new Klant() { Voornaam = "Theo", Familienaam = "Flitser" };
            context.Klanten.Add(k);
            context.SaveChanges();
            w = new Huis() { Huistype = 0, Adres = new Adres() { Straat = "Kasteelstraat", Nummer = 45, Postnummer = 1500, Gemeente = "Asse" }, BouwDatum = DateTime.Now.AddDays(-12), Waarde = 12345, Eigenaar = k };
            context.Woningen.Add(w);
            k = new Klant() { Voornaam = "Bert", Familienaam = "Bibber" };
            context.Klanten.Add(k);

            k = new Data.Klant() { Voornaam = "Piet", Familienaam = "Pienter" };
            context.Klanten.Add(k);
            context.SaveChanges();
            Data.Woning huis = new Data.Huis() { Huistype = 2, Adres = new Data.Adres() { Straat = "Stationstraat", Nummer = 99, Postnummer = 1080, Gemeente = "Elsene" }, BouwDatum = DateTime.Now.AddDays(-129), Waarde = (decimal?)344482.2, Eigenaar = k };
            Data.Woning appart = new Data.Appartement() { Verdieping = 2, Adres = new Data.Adres() { Straat = "Stormstraat", Nummer = 1, Postnummer = 1000, Gemeente = "Brussel" }, BouwDatum = DateTime.Now.AddDays(-512), Waarde = (decimal?)657348.50, Eigenaar = k };

            context.Woningen.Add(huis);
            context.Woningen.Add(appart);

            context.SaveChanges();
        }

        public void Seeding(ImmoWinContext context)
        {
            Seed(context);
        }
    }
}
