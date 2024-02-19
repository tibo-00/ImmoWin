using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IDataObject
    {
        void Lees();
        void Schrijf();
        Boolean DataObjectHasID();
    }
}
