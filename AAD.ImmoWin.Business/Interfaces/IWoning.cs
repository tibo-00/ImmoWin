using Odisee.Common.Observables;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IWoning : IAdresseerbaar, IComparable, IComparable<IWoning>, IFormattable, IObservableObject, IValideerbaar, IDataObject
    {
        DateTime? BouwDatum { get; set; }
        decimal? Waarde { get; set; }
        IKlant Eigenaar { get; set; }
        int CompareToWaarde(IWoning other);
        int CompareToBouwdatum(IWoning other);
    }
}