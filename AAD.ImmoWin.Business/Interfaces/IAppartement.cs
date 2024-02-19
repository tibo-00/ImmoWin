using System;
using System.Collections.Generic;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IAppartement : IWoning, IComparable<IAppartement>
    {
        int Verdieping { get; set; }
        Data.Appartement DataObject { get; set; }
    }
}