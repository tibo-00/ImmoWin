using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Business.Exceptions;
using System.Globalization;
using Odisee.Common.Observables;
using AAD.ImmoWin.Business.Services;

namespace AAD.ImmoWin.Business
{
	public class Klant : ObservableObject, IKlant
	{
		#region Fields

		private String _voornaam;
		private String _familienaam;
		private Woningen _eigendommen;

		#endregion

		#region Properties

		public String Voornaam
		{
			get { return _voornaam; }
			set
			{
				SetProperty(ref _voornaam, value);
			}
		}
		public String Familienaam
		{
			get { return _familienaam; }
			set
			{
				SetProperty(ref _familienaam, value);
			}
		}
		public Woningen Eigendommen
		{
			get { return _eigendommen; }
			private set { _eigendommen = value; }
		}

        public Data.Klant DataObject { get; set; }

        #endregion

        #region Constructors
        public Klant() 
			: this(null, null)
		{}
		public Klant(String familienaam)
			 : this(null, familienaam)
		{}
		public Klant(String voornaam, String familienaam)
		{
			Voornaam = voornaam;
			Familienaam = familienaam;
			InitializeEigendommen();
            DataObject = new Data.Klant();
        }

        public Klant(Data.Klant dataObject)
		{
            InitializeEigendommen();
			DataObject = dataObject;
            Lees();
        }

		private void Eigendommen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged("Eigendommen");
		}

        #endregion

        #region Methods

        public void Lees()
        {
            Voornaam = DataObject.Voornaam;
            Familienaam = DataObject.Familienaam;
            Changed = false;
        }

		public void LeesEigendommen()
		{
            Eigendommen.Clear();
            foreach (Woning woning in WoningenRepository.GetWoningenFromKlant(this))
            {
                Eigendommen.Add(woning);
            }
        }

        public void Schrijf()
        {
			Changed = true;
            if (Changed)
            {
                DataObject.Voornaam = Voornaam;
                DataObject.Familienaam = Familienaam;
                Changed = false;
            }
        }
        private void InitializeEigendommen()
        {
            Eigendommen = new Woningen();
            Eigendommen.CollectionChanged += Eigendommen_CollectionChanged;
        }

        public List<Exception> Validate()
		{
			List<Exception> exceptions = new List<Exception>();

			if (String.IsNullOrEmpty(Familienaam))
			{
				exceptions.Add(new FamilienaamLeeg_KlantException());
			}

            if (String.IsNullOrEmpty(Voornaam))
			{
                exceptions.Add(new VoornaamLeeg_KlantException());
            }
            return exceptions;
        }

		public Boolean DataObjectHasID()
		{
			if (DataObject.Id  == 0)
			{
				return false;
			}
			return true;
		}


        public override string ToString()
		{
			return ToString(null);
		}

		#endregion

		#region Interfaces

		#region Interfaces.IComparable

		public int CompareTo(object obj)
		{
			if (obj is IKlant other)
				return CompareTo(other);
			throw new ArgumentException("Iklant.CompareTo(object obj): obj moet een IKlant zijn.");
		}

		#endregion

		#region Interfaces.IComparable<IKlant>

		public int CompareTo(IKlant other)
		{
			int result;

			if ((result = Familienaam.CompareTo(other.Familienaam)) == 0)
				result = Voornaam.CompareTo(other.Voornaam);

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
					result = $"{Familienaam} {Voornaam} #eigendommen: {Eigendommen.Count}";
					break;
				case "VF": // voornaam familienaam
					result = $"{Voornaam} {Familienaam}".Trim();
					break;
			}

			return result;
		}

		#endregion

		#region Interfaces.ICloneable

		public object Clone()
		{
			Klant clone = (Klant)MemberwiseClone();
			return clone;
		}

		#endregion

		#endregion
	}
}
