using AAD.ImmoWin.Business.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Data.Classes
{

    public class Huis : Woning
    {
        public Huistype Huistype { get; set; }
    }
}
