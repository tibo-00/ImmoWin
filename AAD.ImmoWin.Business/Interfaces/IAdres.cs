using Odisee.Common.Observables;
using System;
using System.Collections.Generic;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IAdres : IFormattable, IComparable, IComparable<IAdres>, IObservableObject, IValideerbaar
    {
        string Gemeente { get; set; }
        int Nummer { get; set; }
        int Postnummer { get; set; }
        string Straat { get; set; }
    }
}