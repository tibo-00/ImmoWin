using AAD.ImmoWin.Business;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Business.Services;
using Odisee.Common.Commands;
using Odisee.Common.Mediators;
using Odisee.Common.ViewModels;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AAD.ImmoWin.WpfApp.ViewModels
{
    public class WoningDetailCommandViewModel : BaseViewModel
    {
        #region Properties

        #region Command properties

        public RelayCommand WoningWijzigingBewarenCommand { get; }
        public RelayCommand WoningWijzigingAnnulerenCommand { get; }

        #endregion

        #region Observable properties

        private IWoning _woning;
        public IWoning Woning
        {
            get { return _woning; }
            set
            {
                IsEnabled = false;
                SetProperty(ref _woning, value);
                IsEnabled = false;

            }
        }

        private BaseViewModel _woningDetailView;
        public BaseViewModel WoningDetailView
        {
            get { return _woningDetailView; }
            set
            {
                SetProperty(ref _woningDetailView, value);
            }
        }

        #endregion

        #endregion

        #region Constructors

        public WoningDetailCommandViewModel()
        {
            // Observable properties
            Title = "Woning detailgegevens";
            IsEnabled = false;

            // Commands
            WoningWijzigingBewarenCommand = new RelayCommand(WoningWijzigingBewarenCommandExecute, WoningWijzigingBewarenCommandCanExecute);
            WoningWijzigingAnnulerenCommand = new RelayCommand(WoningWijzigingAnnulerenCommandExecute);

            Mediator.GetInstance().Register(this, "Woning geselecteerd");
            Mediator.GetInstance().Register(this, "Woning wijzigen");
            Mediator.GetInstance().Register(this, "Woning wijzigen annuleren");
            Mediator.GetInstance().Register(this, "Woning opslaan");
            Mediator.GetInstance().Register(this, "Huis toevoegen");
            Mediator.GetInstance().Register(this, "Appartement toevoegen");
        }

        #endregion

        #region Methods

        #region Command Methods

        private void WoningWijzigingBewarenCommandExecute()
        {
            StringBuilder sb = new StringBuilder();
            Woning.Validate();
            foreach (Exception ex in Woning.Validate())
            {
                sb.AppendLine(ex.Message);
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (Woning.DataObjectHasID())
                {
                    WoningenRepository.UpdateWoning(Woning);
                }
                else
                {
                    WoningenRepository.AddWoning(Woning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Mediator.GetInstance().Notify(this, "Woning opslaan", null);

        }
        private Boolean WoningWijzigingBewarenCommandCanExecute()
        {
            return (Woning?.Changed ?? false) || (Woning?.Adres?.Changed ?? false);
        }

        private void WoningWijzigingAnnulerenCommandExecute()
        {
            if (Woning != null && Woning.DataObjectHasID() )
            {
                Woning.Lees();
            }
            Mediator.GetInstance().Notify(this, "Woning wijzigen annuleren", null);
        }

        #endregion

        #region IMediator Methods
        public override void Notify(object from, string message, object data)
        {
            switch (message)
            {
                case "Woning geselecteerd":
                    Woning = (IWoning)data;
                    if (Woning is IHuis huis)
                    {
                        Title = "Huis detailgegevens";
                        WoningDetailView = new HuisDetailViewModel(huis);
                    } else if (Woning is IAppartement appartement)
                    {
                        Title = "Appartement detailgegevens";
                        WoningDetailView = new AppartementDetailViewModel(appartement);
                    }
                    break;
                case "Huis toevoegen":
                    Woning = new Huis() { Adres = new Adres() };
                    Title = "Huis detailgegevens";
                    WoningDetailView = new HuisDetailViewModel((IHuis)Woning);
                    IsEnabled = true;
                    break;
                case "Appartement toevoegen":
                    Woning = new Appartement() { Adres = new Adres() };
                    Title = "Appartement detailgegevens";
                    WoningDetailView = new AppartementDetailViewModel((IAppartement)Woning);
                    IsEnabled = true;
                    break;
                case "Woning wijzigen":
                    IsEnabled = true;
                    break;
                case "Woning opslaan":
                case "Woning wijzigen annuleren":
                    bool test = IsEnabled;
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
