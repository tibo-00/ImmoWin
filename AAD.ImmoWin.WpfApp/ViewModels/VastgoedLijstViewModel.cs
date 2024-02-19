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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AAD.ImmoWin.WpfApp.ViewModels
{
    public class VastgoedLijstViewModel : BaseViewModel
    {
        #region Properties

        #region Command properties
        public RelayCommand HuisToevoegenCommand { get; set; }
        public RelayCommand AppartementToevoegenCommand { get; set; }
        public RelayCommand VastgoedWijzigenCommand { get; set; }
        public RelayCommand VastgoedVerwijderenCommand { get; set; }
        public RelayCommand EigenaarResetCommand { get; set; }
        public RelayCommand SortAdresCommand { get; set; }
        public RelayCommand SortWaardeCommand { get; set; }
        public RelayCommand SortBouwdatumCommand { get; set; }

        #endregion

        #region Observable properties

        private Woningen _woningen;

        public Woningen Woningen
        {
            get { return _woningen; }
            set
            {
                SetProperty(ref _woningen, value);
            }
        }

        private IWoning _geselecteerdeWoning;

        public IWoning GeselecteerdeWoning
        {
            get { return _geselecteerdeWoning; }
            set
            {
                if (SetProperty(ref _geselecteerdeWoning, value))
                {
                    if (GeselecteerdeWoning != null && GeselecteerdeWoning.Eigenaar != null)
                    {
                        Mediator.GetInstance().Notify(this, "Woning geselecteerd", GeselecteerdeWoning);
                    }
                }
            }
        }

        private IKlant _selectedEigenaar;
        public IKlant SelectedEigenaar
        {
            get { return _selectedEigenaar; }
            set
            {
                SetProperty(ref _selectedEigenaar, value);
                KiesTypeVastgoeden();
            }
        }

        public Klanten KlantenLijst { get; set; }

        private int _selectedVastgoedType;

        public int SelectedVastgoedType
        {
            get { return _selectedVastgoedType; }
            set
            {
                SetProperty(ref _selectedVastgoedType, value);
                KiesTypeVastgoeden();
            }
        }

        #endregion

        #endregion

        #region Constructors

        public VastgoedLijstViewModel(Woningen woningen)
        {
            // Observable properties
            Title = "Lijst woningen";
            Woningen = woningen;
            KlantenLijst = KlantenRepository.GetKlantenMetEigendommen();


            // Commands
            HuisToevoegenCommand = new RelayCommand(HuisToevoegenCommandExecute);
            AppartementToevoegenCommand = new RelayCommand(AppartementToevoegenCommandExecute);
            VastgoedWijzigenCommand = new RelayCommand(VastgoedWijzigenCommandExecute, VastgoedWijzigenCommandCanExecute);
            VastgoedVerwijderenCommand = new RelayCommand(VastgoedVerwijderenCommandExecute, VastgoedVerwijderenCommandCanExecute);
            EigenaarResetCommand = new RelayCommand(EigenaarResetCommandExecute, EigenaarResetCommandCanExecute);
            SortAdresCommand = new RelayCommand(SortAdresCommandExecute);
            SortWaardeCommand = new RelayCommand(SortWaardeCommandExecute);
            SortBouwdatumCommand = new RelayCommand(SortBouwdatumCommandExecute);

            Mediator.GetInstance().Register(this, "Woning wijzigen");
            Mediator.GetInstance().Register(this, "Woning opslaan");
            Mediator.GetInstance().Register(this, "Woning wijzigen annuleren");
            Mediator.GetInstance().Register(this, "Huis toevoegen");
            Mediator.GetInstance().Register(this, "Appartement toevoegen");
        }

        #endregion

        #region Methods

        #region Command  methods
        private void HuisToevoegenCommandExecute()
        {
            Mediator.GetInstance().Notify(this, "Huis toevoegen", null);
            
        }
        private void AppartementToevoegenCommandExecute()
        {
            Mediator.GetInstance().Notify(this, "Appartement toevoegen", null);
        }

        private void VastgoedWijzigenCommandExecute()
        {
            Mediator.GetInstance().Notify(this, "Woning wijzigen", null);
        }
        private Boolean VastgoedWijzigenCommandCanExecute()
        {
            return GeselecteerdeWoning != null;
        }

        private void VastgoedVerwijderenCommandExecute()
        {
            try
            {
                WoningenRepository.RemoveWoning(GeselecteerdeWoning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            KiesTypeVastgoeden();
        }
        private Boolean VastgoedVerwijderenCommandCanExecute()
        {
            return GeselecteerdeWoning != null;
        }

        private void EigenaarResetCommandExecute()
        {
            SelectedEigenaar = null;
            KiesTypeVastgoeden();
        }
        private Boolean EigenaarResetCommandCanExecute()
        {
            return SelectedEigenaar != null;
        }

        private void SortAdresCommandExecute()
        {
            List<IWoning> woningenLijst = new List<IWoning>();
            foreach (IWoning woning in Woningen)
            {
                woningenLijst.Add(woning);
            }
            woningenLijst.Sort();
            Woningen = new Woningen(woningenLijst);
        }

        private void SortWaardeCommandExecute()
        {
            List<IWoning> woningenLijst = new List<IWoning>();
            foreach(IWoning woning in Woningen)
            {
                woningenLijst.Add(woning);
            }
            Comparison<IWoning> compareByWaarde = (x, y) => x.CompareToWaarde(y);
            woningenLijst.Sort(compareByWaarde);
            Woningen = new Woningen(woningenLijst);
        }

        private void SortBouwdatumCommandExecute()
        {
            List<IWoning> woningenLijst = new List<IWoning>();
            foreach (IWoning woning in Woningen)
            {
                woningenLijst.Add(woning);
            }
            Comparison<IWoning> compareByBouwDatum = (x, y) => x.CompareToBouwdatum(y);
            woningenLijst.Sort(compareByBouwDatum);
            Woningen = new Woningen(woningenLijst);
        }

        private void KiesTypeVastgoeden()
        {
            GeselecteerdeWoning = null;
            switch (SelectedVastgoedType)
            {
                case 1:
                    Woningen = WoningenRepository.GetHuizen();
                    break;
                case 2:
                    Woningen = WoningenRepository.GetAppartementen();
                    break;
                default:
                    Woningen = WoningenRepository.GetWoningen();
                    break;
            }
            if(SelectedEigenaar != null)
            {
                Woningen = WoningenRepository.GetWoningenFromKlant(SelectedEigenaar);
                switch (SelectedVastgoedType)
                {
                    case 1:
                        Woningen = new Woningen(Woningen.Where(woning => (woning is Huis)).ToList());
                        break;
                    case 2:
                        Woningen = new Woningen(Woningen.Where(woning => (woning is Appartement)).ToList());
                        break;
                }
            }
        }

        #endregion

        #region IMediator Methods
        public override void Notify(object from, string message, object data)
        {
            switch (message)
            {
                case "Huis toevoegen":
                case "Appartement toevoegen":
                case "Woning wijzigen":
                    IsEnabled = false;
                    break;
                case "Woning opslaan":
                case "Woning wijzigen annuleren":
                    KiesTypeVastgoeden();
                    KlantenLijst = KlantenRepository.GetKlantenMetEigendommen();
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
