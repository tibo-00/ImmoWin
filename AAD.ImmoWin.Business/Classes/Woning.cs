using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Business.Exceptions;
using Odisee.Common.Observables;

namespace AAD.ImmoWin.Business
{
    public abstract class Woning : ObservableObject, IWoning
    {
        #region Fields

        private IAdres _adres;
        private IKlant _eigenaar;
        private DateTime? _bouwDatum;
        private Decimal? _waarde;

        #endregion

        #region Properties

        public Decimal? Waarde
        {
            get { return _waarde; }
            set
            {
                SetProperty(ref _waarde, value);
            }
        }
        public DateTime? BouwDatum
        {
            get { return _bouwDatum; }
            set
            {
                SetProperty(ref _bouwDatum, value);
            }
        }
        public IAdres Adres
        {
            get { return _adres; }
            set
            {
                SetProperty(ref _adres, value);
            }
        }
        public IKlant Eigenaar
        {
            get { return _eigenaar; }
            set
            {
                SetProperty(ref _eigenaar, value);
            }
        }

        #endregion

        #region Constructors

        public Woning(Adres adres, DateTime? bouwdatum = null, Decimal? waarde = null)
        {
            Adres = adres;
            BouwDatum = BouwDatum;
            Waarde = waarde;
        }

        public Woning()
        {}

        #endregion

        #region Methods

        public virtual List<Exception> Validate()
        {
            List<Exception> exceptions = new List<Exception>();

            if (Waarde.HasValue && Waarde < 0)
            {
                exceptions.Add(new WaardeTeKlein_WoningException());
            }

            if ((!BouwDatum.HasValue) || (BouwDatum > DateTime.Now))
            {
                exceptions.Add(new BouwdatumTeGroot_WoningException());
            }
            if (Eigenaar == null)
            {
                exceptions.Add(new EigenaarLeeg_WoningException());
            }

            exceptions.AddRange(Adres.Validate());

            return exceptions;
        }
        public abstract void Lees();
        public abstract void Schrijf();
        public abstract bool DataObjectHasID();

        public override string ToString()
        {
            return ToString(null);
        }

        public virtual int CompareToWaarde(IWoning other)
        {
            return Nullable.Compare<decimal>(Waarde, other.Waarde);
        }

        public virtual int CompareToBouwdatum(IWoning other)
        {
            return Nullable.Compare<DateTime>(BouwDatum, other.BouwDatum);
        }

        #endregion

        #region Interfaces

        #region Interfaces.CompareTo

        public int CompareTo(object obj)
        {
            if (obj is IWoning other)
                return CompareTo(other);
            throw new ArgumentException("IWoning.CompareTo(object obj): obj moet IWoning zijn");
        }

        #endregion

        #region Interfaces.CompareTo<IWoning>

        public virtual int CompareTo(IWoning other)
        {
            return Adres.CompareTo(other.Adres);
        }

        #endregion

        #region Interfaces.IFormattable

        public virtual String ToString(String format)
        {
            return ToString(format, null);
        }

        public virtual string ToString(string format, IFormatProvider formatProvider)
        {
            String result = null;

            if (formatProvider is null)
                formatProvider = CultureInfo.CurrentCulture;
            if (format == null)
                format = "T"; // typical
            switch (format)
            {
                case "T":
                    result = $"€ {Waarde} - {Adres}";
                    break;
            }
            return result;

        }

        #endregion

        #endregion
    }
}
