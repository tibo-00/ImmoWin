using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace AAD.ImmoWin.Data
{
    public class WoningenRepository
    {
        public static List<Woning> GetWoningen()
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                return context.Woningen.Include(woning => woning.Adres).Include(woning => woning.Eigenaar).ToList();
            }
        }

        public static List<Woning> GetWoningenFromKlant(int klantId)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                return context.Woningen.Include(woning => woning.Adres).Include(woning => woning.Eigenaar).Where(woning => woning.Eigenaar.Id == klantId).ToList();
            }
        }

        public static List<Huis> GetHuizen()
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                return context.Woningen.OfType<Huis>().Include(woning => woning.Adres).Include(woning => woning.Eigenaar).ToList();
            }
        }

        public static List<Appartement> GetAppartementen()
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                return context.Appartementen.Include(woning => woning.Adres).Include(woning => woning.Eigenaar).ToList();
            }
        }

        public static Woning AddWoning(Woning w)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                int eigenaarId = w.Eigenaar.Id;
                Klant existingEigenaar = context.Klanten.Find(eigenaarId);

                if (existingEigenaar != null)
                {
                    w.Eigenaar = existingEigenaar;
                    context.Woningen.Add(w);
                    context.SaveChanges();
                }
                return w;
            }
        }

        public static Woning UpdateWoning(Woning changedWoning)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                int eigenaarId = changedWoning.Eigenaar.Id;
                Klant existingEigenaar = context.Klanten.Find(eigenaarId);

                changedWoning.KlantId = changedWoning.Eigenaar.Id;

                int adresId = changedWoning.Adres.Id;
                Adres existingAdres = context.Adressen.Find(adresId);

                int woningId = changedWoning.Id;
                Woning existingWoning = context.Woningen.Find(woningId);
                if ((existingEigenaar != null) && (existingAdres != null) && (existingWoning != null))
                {
                    context.Entry(existingEigenaar).State = EntityState.Detached;
                    context.Entry(existingAdres).State = EntityState.Detached;
                    context.Entry(existingWoning).State = EntityState.Detached;

                    //context.Entry(changedWoning.Eigenaar).State = EntityState.Modified;
                    context.Entry(changedWoning.Adres).State = EntityState.Modified;
                    context.Entry(changedWoning).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return changedWoning;
            }
        }

        public static void RemoveWoning(Woning w)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                int adresId = w.Adres.Id;
                Adres existingAdres = context.Adressen.Find(adresId);

                int woningId = w.Id;
                Woning existingWoning = context.Woningen.Find(woningId);

                if ((existingAdres != null) && (existingWoning != null))
                {
                    context.Adressen.Remove(existingAdres);
                    context.Woningen.Remove(existingWoning);
                    context.SaveChanges();
                }
            }
        }


        public static void FindWoning(int id)
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                context.Woningen.FirstOrDefault(Woning => Woning.Id == id);
            }
        }
    }
}
