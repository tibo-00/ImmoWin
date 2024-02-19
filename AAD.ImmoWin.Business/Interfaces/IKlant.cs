using Odisee.Common.Observables;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AAD.ImmoWin.Business.Interfaces
{
    public interface IKlant : IFormattable, IComparable, IComparable<IKlant>, IObservableObject, ICloneable, IValideerbaar, IDataObject
    {
        Woningen Eigendommen { get; }
        string Familienaam { get; set; }
        string Voornaam { get; set; }
        Data.Klant DataObject { get; set; }
        void LeesEigendommen();
    }
}