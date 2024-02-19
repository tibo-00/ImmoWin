using AAD.ImmoWin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Services
{
    public class SeedingService
    {
        public static void EmptyDatabase()
        {
            Data.SeedingService.EmptyDatabase();
        }

        public static void DefaultDatabase()
        {
            Data.SeedingService.DefaultDatabase();
        }
    }
}
