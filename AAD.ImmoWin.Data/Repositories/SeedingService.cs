using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data
{
    public class SeedingService
    {
        public static void EmptyDatabase()
        {
            using (ImmoWinContext context = new ImmoWinContext())
            {
                context.Woningen.RemoveRange(context.Woningen);
                context.Huizen.RemoveRange(context.Huizen);
                context.Appartementen.RemoveRange(context.Appartementen);
                context.Adressen.RemoveRange(context.Adressen);
                context.Klanten.RemoveRange(context.Klanten);

                context.SaveChanges();
            }
        }

        public static void DefaultDatabase()
        {
            EmptyDatabase();
            using (ImmoWinContext context = new ImmoWinContext())
            {
                DropCreateImmoWinContextContextIfModelChanges seeder = new DropCreateImmoWinContextContextIfModelChanges();
                seeder.Seeding(context);
            }
        }
    }
}
