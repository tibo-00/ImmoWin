using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAD.ImmoWin.Business.Exceptions;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Data;

namespace AAD.ImmoWin.Business
{
    public class Huis : Woning, IHuis
    {
        #region Fields

        private Huistype _huistype;

        #endregion

        #region Properties

        public Huistype Type
        {
            get { return _huistype; }
            set { SetProperty(ref _huistype, value); }
        }

        public Data.Huis DataObject { get; set; }

        #endregion

        #region Constructors

        public Huis(Huistype huistype, Adres adres, DateTime? bouwdatum = null, decimal? waarde = null)
            : base(adres, bouwdatum, waarde)
        {
            Type = huistype;
            DataObject = new Data.Huis() { Adres = new Data.Adres() };
        }

        public Huis()
        {
            DataObject = new Data.Huis() { Adres = new Data.Adres() };
        }

        public Huis(Data.Huis dataObject)
        {
            DataObject = dataObject;
            Lees();
        }

        public Huis(Data.Huis dataObject, IKlant eigenaar)
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
            Type = (Huistype)DataObject.Huistype;
            if (Eigenaar is null)
            {
                Eigenaar = new Klant(DataObject.Eigenaar);
            }
            Changed = false;
            Adres.Changed = false;
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
                DataObject.Huistype = (int)Type;
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

            if (!Enum.IsDefined(typeof(Huistype), Type)) 
            {
                exceptions.Add(new HuisTypeLeeg_HuisException());
            }

            return exceptions;
        }

        public override string ToString()
        {
            return $"{GetType().Name}: type: {Type} - {base.ToString()}";
        }

        #endregion

        #region Interfaces

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
                    result = $"{GetType().Name} {Type} - {base.ToString(null, null)}";
                    break;
            }

            return result;
        }

        #endregion

        #endregion

    }
}
