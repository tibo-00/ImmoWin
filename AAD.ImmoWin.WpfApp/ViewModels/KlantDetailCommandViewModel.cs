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
	public class KlantDetailCommandViewModel : BaseViewModel
	{
		#region Properties

		#region Command properties

		public RelayCommand KlantWijzigingBewarenCommand { get; }
		public RelayCommand KlantWijzigingAnnulerenCommand { get; }

		#endregion

		#region Observable properties

		private IKlant _klant;
		public IKlant Klant
		{
			get { return _klant; }
			set
			{
				SetProperty(ref _klant, value);
			}
		}

		#endregion

		#endregion

		#region Constructors

		public KlantDetailCommandViewModel()
		{
			// Observable properties
			Title = "Klant detailgegevens";
			IsEnabled = false;

			// Commands
			KlantWijzigingBewarenCommand = new RelayCommand(KlantWijzigingBewarenCommandExecute, KlantWijzigingBewarenCommandCanExecute);
			KlantWijzigingAnnulerenCommand = new RelayCommand(KlantWijzigingAnnulerenCommandExecute);

			Mediator.GetInstance().Register(this, "Klant geselecteerd");
            Mediator.GetInstance().Register(this, "Klant wijzigen");
            Mediator.GetInstance().Register(this, "Klant wijzigen annuleren");
            Mediator.GetInstance().Register(this, "Klant opslaan");
            Mediator.GetInstance().Register(this, "Klant toevoegen");
        }

        #endregion

        #region Methods

        #region Command Methods

        private void KlantWijzigingBewarenCommandExecute()
		{
			
            StringBuilder sb = new StringBuilder();
            foreach (Exception ex in Klant.Validate())
            {
                sb.AppendLine(ex.Message);
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
            }
            Klant.Schrijf();
            try
            {
                if (Klant.DataObjectHasID())
                {
                    KlantenRepository.UpdateKlant(Klant);
                }
                else
                {
                    KlantenRepository.AddKlant(Klant);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Mediator.GetInstance().Notify(this, "Klant opslaan", null);

        }
        private Boolean KlantWijzigingBewarenCommandCanExecute()
		{
			return Klant?.Changed ?? false;
		}

		private void KlantWijzigingAnnulerenCommandExecute()
		{
			Klant.Lees();
			Mediator.GetInstance().Notify(this, "Klant wijzigen annuleren", null);
		}

        #endregion

        #region IMediator Methods
        public override void Notify(object from, string message, object data)
        {
			switch (message)
			{
				case "Klant geselecteerd":
					Klant = (Klant)data;
					break;
                case "Klant toevoegen":
					Klant = new Klant();
                    IsEnabled = true;
                    break;
                case "Klant wijzigen":
					IsEnabled = true;
					break;
                case "Klant opslaan":
                case "Klant wijzigen annuleren":
					IsEnabled = false;
					break;
                default:
					break;
			}
		}
        #endregion

        #endregion
    }
}
