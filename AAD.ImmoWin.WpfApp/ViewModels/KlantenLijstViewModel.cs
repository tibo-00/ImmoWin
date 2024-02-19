using AAD.ImmoWin.Business;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Business.Services;
using Odisee.Common.Commands;
using Odisee.Common.Mediators;
using Odisee.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAD.ImmoWin.WpfApp.ViewModels
{
	public class KlantenLijstViewModel : BaseViewModel
	{
		#region Properties

		#region Command properties
		public RelayCommand KlantToevoegenCommand { get; set; }
		public RelayCommand KlantWijzigenCommand { get; set; }
		public RelayCommand KlantVerwijderenCommand { get; set; }

		#endregion

		#region Observable properties

		private Klanten _klanten;

		public Klanten Klanten
		{
			get { return _klanten; }
			set
			{
				SetProperty(ref _klanten, value);
			}
		}

		private IKlant _geselecteerdeKlant;

		public IKlant GeselecteerdeKlant
		{
			get { return _geselecteerdeKlant; }
			set
			{
				if(SetProperty(ref _geselecteerdeKlant, value))
				{
					if(GeselecteerdeKlant != null)
					{
						Mediator.GetInstance().Notify(this, "Klant geselecteerd", GeselecteerdeKlant);
                    }
                }
            }
		}

		#endregion

		#endregion

		#region Constructors

		public KlantenLijstViewModel(Klanten klanten)
		{
			// Observable properties
			Title = "Lijst klanten";
			Klanten = klanten;
			
			// Commands
			KlantToevoegenCommand = new RelayCommand(KlantToevoegenCommandExecute);
			KlantWijzigenCommand = new RelayCommand(KlantWijzigenCommandExecute, KlantWijzigenCommandCanExecute);
			KlantVerwijderenCommand = new RelayCommand(KlantVerwijderenCommandExecute, KlantVerwijderenCommandCanExecute);

            Mediator.GetInstance().Register(this, "Klant wijzigen");
            Mediator.GetInstance().Register(this, "Klant opslaan");
            Mediator.GetInstance().Register(this, "Klant wijzigen annuleren");
            Mediator.GetInstance().Register(this, "Klant toevoegen");
        }

        #endregion

        #region Methods

        #region Command  methods

        private void KlantToevoegenCommandExecute()
		{
            Mediator.GetInstance().Notify(this, "Klant toevoegen", null);
        }

		private void KlantWijzigenCommandExecute()
		{
			Mediator.GetInstance().Notify(this, "Klant wijzigen", null);
		}
		private Boolean KlantWijzigenCommandCanExecute()
		{
			return GeselecteerdeKlant != null;
		}

		private void KlantVerwijderenCommandExecute()
		{
            try
            {
                KlantenRepository.RemoveKlant(GeselecteerdeKlant);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}

		private Boolean KlantVerwijderenCommandCanExecute()
		{
			return (GeselecteerdeKlant != null) && (GeselecteerdeKlant.Eigendommen.Count == 0);
		}

        #endregion

        #region IMediator Methods
        public override void Notify(object from, string message, object data)
        {
            switch (message)
            {
				case "Klant toevoegen":
                case "Klant wijzigen":
                    IsEnabled = false;
                    break;
				case "Klant opslaan":
                case "Klant wijzigen annuleren":
                    IsEnabled = true;
                    break;
                default:
                    break;
            }
        }
        #endregion

        #endregion
    }
}
