using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Classes
{
    internal class ApiResponse
    {
        public DateTime DateTime { get; set; }
        public Dictionary<string, Currency> APIResponse { get; set; }
    }
}
