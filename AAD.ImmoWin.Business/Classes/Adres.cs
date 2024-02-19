using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Business.Exceptions;
using Odisee.Common.Observables;

namespace AAD.ImmoWin.Business
{
    public class Adres : ObservableObject, IAdres
    {
        #region Fields

        private String _straat;
        private int _nummer;
        private int _postnummer;
        private String _gemeente;

        #endregion

        #region Properties

        public String Straat
        {
            get { return _straat; }
            set
            {
                SetProperty(ref _straat, value);
            }
        }
        public int Nummer
        {
            get { return _nummer; }
            set
            {
                SetProperty(ref _nummer, value);
            }
        }
        public int Postnummer
        {
            get { return _postnummer; }
            set
            {
                SetProperty(ref _postnummer, value);
            }
        }
        public String Gemeente
        {
            get { return _gemeente; }
            set
            {
                SetProperty(ref _gemeente, value);
            }
        }

        #endregion

        #region Constructor

        public Adres(string straat, int nummer, int postnummer, string gemeente)
        {
            Straat = straat;
            Nummer = nummer;
            Postnummer = postnummer;
            Gemeente = gemeente;
        }
        public Adres()
        {
            
        }

        #endregion, 

        #region Methods

        public List<Exception> Validate()
        {
            List<Exception> exceptions = new List<Exception>();
            if (String.IsNullOrEmpty(Straat))
            {
                exceptions.Add(new StraatLeeg_AdresException());
            }
            if (String.IsNullOrEmpty(Gemeente))
            {
                exceptions.Add(new GemeenteLeeg_AdresException());
            }
            if (Nummer <= 0)
            {
                exceptions.Add(new NummerTeKlein_AdresException());
            }
            if (Postnummer <= 0)
            {
                exceptions.Add(new PostnummerTeKlein_AdresException());
            }
            return exceptions;
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        #endregion

        #region Interfaces

        #region Interfaces.IComparable

        public int CompareTo(object obj)
        {
            if (obj is IAdres other)
                return CompareTo(other);
            throw new ArgumentException("IAdres.CompareTo(object obj): obj moet IAdres zijn.");
        }

        #endregion

        #region Interfaces.IComparable<IAdres>

        public int CompareTo(IAdres other)
        {
            int result;
            if ((result = Postnummer.CompareTo(other.Postnummer)) == 0)
                if((result = Gemeente.CompareTo(other.Gemeente)) == 0)
                    if ((result=Straat.CompareTo(other.Straat)) == 0)
                        result = Nummer.CompareTo(other.Nummer);
            return result;
        }

        #endregion

        #region Interfaces.IFormattable

        public String ToString(String format)
        {
            return ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            String result = null;

            if (formatProvider is null)
                formatProvider = CultureInfo.CurrentCulture;
            if (format == null)
                format = "T"; // typical
            switch (format)
            {
                default:
                case "T": // typical
                    result = $"{Straat} {Nummer}, {Postnummer} {Gemeente}";
                    break;
                case "S": // sort
                    result = $"{Postnummer} {Gemeente} {Straat} {Nummer}";
                    break;
                case "L": // lokaal
                    result = $"{Straat} {Nummer}";
                    break;
            }

            return result;
        }

        #endregion

        #endregion
    }
}
