using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data
{
    public class KlantenRepository
    {
        public static List<Klant> GetKlanten()
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                return context.Klanten.ToList();
            }
        }

        public static Klant AddKlant(Klant k)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                context.Klanten.Add(k);
                context.SaveChanges();
                return k;
            }
        }

        public static Klant UpdateKlant(Klant changedKlant)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                context.Klanten.Attach(changedKlant);
                context.Entry(changedKlant).State = EntityState.Modified;
                context.SaveChanges();

                return changedKlant;
            }
        }

        public static void RemoveKlant(Klant k)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                context.Klanten.Attach(k);
                context.Klanten.Remove(k);
                context.SaveChanges();
            }
        }


        public static void FindKlant(int id)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                context.Klanten.FirstOrDefault(klant => klant.Id == id);
            }
        }

        public static List<Klant> SearchKlant(String search)
        {
            String[] searchWords = search.ToLower().Split(' ');

            using (var context = new ImmoWinContext())
            {
                return context.Klanten
                    .Where(klant => searchWords.All(word => klant.Voornaam.ToLower().Contains(word) || klant.Familienaam.ToLower().Contains(word)))
                    .ToList();
            }
        }
    }
}
