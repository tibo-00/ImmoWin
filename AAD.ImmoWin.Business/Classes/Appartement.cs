using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAD.ImmoWin.Business.Exceptions;
using AAD.ImmoWin.Business.Interfaces;

namespace AAD.ImmoWin.Business
{
    public class Appartement : Woning, IAppartement
    {
        #region Fields

        private int _verdieping;

        #endregion

        #region Properties

        public int Verdieping
        {
            get { return _verdieping; }
            set { SetProperty(ref _verdieping, value); }
        }

        public Data.Appartement DataObject { get; set; }

        #endregion

        #region Constructors

        public Appartement(int verdieping, Adres adres, DateTime? bouwdatum = null, decimal? waarde = null) : base(adres, bouwdatum, waarde)
        {
            Verdieping = verdieping;
            DataObject = new Data.Appartement() { Adres = new Data.Adres() };
        }

        public Appartement()
        {
            DataObject = new Data.Appartement() { Adres = new Data.Adres()};
        }

        public Appartement(Data.Appartement dataObject)
        {
            DataObject = dataObject;
            Lees();
        }

        public Appartement(Data.Appartement dataObject, IKlant eigenaar)
        {
            DataObject = dataObject;
            Eigenaar = eigenaar;
            Lees();
        }


        #endregion

        #region Methods

        public override void Lees()
        {
            Adres = new Adres(DataObject.Adres.Straat, DataObject.Adres.Nummer, DataObject.Adres.Postnummer, DataObject.Adres.Gemeente);
            BouwDatum = DataObject.BouwDatum;
            Waarde = DataObject.Waarde;
            Verdieping = DataObject.Verdieping;
            if (Eigenaar is null)
            {
                Eigenaar = new Klant(DataObject.Eigenaar);
            }
            Changed = false;
        }

        public override void Schrijf()
        {
            if (Changed || Adres.Changed)
            {
                DataObject.Adres.Postnummer = Adres.Postnummer;
                DataObject.Adres.Straat = Adres.Straat;
                DataObject.Adres.Nummer = Adres.Nummer;
                DataObject.Adres.Gemeente = Adres.Gemeente;
                DataObject.BouwDatum = BouwDatum;
                DataObject.Waarde = Waarde;
                DataObject.Verdieping = Verdieping;
                DataObject.Eigenaar = Eigenaar.DataObject; // id van data object wordt behouden
                Changed = false;
            }
        }
        public override Boolean DataObjectHasID()
        {
            if (DataObject is null || DataObject.Id == 0)
            {
                return false;
            }
            return true;
        }

        public override List<Exception> Validate()
        {
            List<Exception> exceptions = new List<Exception>();

            exceptions.AddRange(base.Validate());

            if (Verdieping < 0)
            {
                exceptions.Add(new VerdiepingLeeg_AppartementException());
            }


            return exceptions;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: verd. {Verdieping} - {base.ToString()}";
        }

        #endregion

        #region Interfaces

        #region Interfaces.CompareTo<IWoning>

        public override int CompareTo(IWoning other)
        {
            int result;
            if ((result = base.CompareTo(other)) == 0 && other is Appartement a)
                result = CompareTo(a);
            return result;
        }

        #endregion

        #region Interfaces.CompareTo<IAppartement>

        public int CompareTo(IAppartement other)
        {
            int result;
            if ((result = base.CompareTo(other)) == 0)
                result = Verdieping.CompareTo(other.Verdieping);
            return result;
        }


        #endregion 

        #region Interfaces.IFormattable

        public override String ToString(String format)
        {
            return ToString(format, null);
        }

        public override string ToString(string format, IFormatProvider formatProvider)
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
                    result = $"{GetType().Name} verd. {Verdieping} - {base.ToString(null, null)}";
                    break;
            }

            return result;
        }

        #endregion

        #endregion

    }
}
