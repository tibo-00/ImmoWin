using AAD.ImmoWin.Business;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Business.Services;
using Odisee.Common.Commands;
using Odisee.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AAD.ImmoWin.WpfApp.ViewModels
{
    public class AppartementDetailViewModel : BaseViewModel
    {
        public Klanten KlantenLijst { get; set; }

        private IKlant _selectedEigenaar;
        public IKlant SelectedEigenaar
        {
            get { return _selectedEigenaar; }
            set
            {
                SetProperty(ref _selectedEigenaar, value);
                Woning.Eigenaar = _selectedEigenaar;
            }
        }

        public IAppartement Woning { get; set; }

        private decimal _convertedWaarde;

        public decimal ConvertedWaarde
        {
            get { return _convertedWaarde; }
            set 
            {
                SetProperty(ref _convertedWaarde, value);
            }
        }


        public List<String> CurrencyLijst { get; set; }

        private String _selectedCurrency;

        public String SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                SetProperty(ref _selectedCurrency, value);
                ConvertCurrency();
            }
        }

        public AppartementDetailViewModel(IAppartement woning)
        {
            Woning = woning;
            KlantenLijst = KlantenRepository.GetKlantenMetEigendommen();

            CurrencyLijst = CurrencyConverterService.GetCurrencyList("EUR");
        }

        private void ConvertCurrency()
        {
            decimal? waarde = CurrencyConverterService.ConvertFromEuroTo(SelectedCurrency, Woning.Waarde);
            if (waarde != null)
            {
                ConvertedWaarde = (decimal)waarde;
            }
            ConvertedWaarde = Math.Round(ConvertedWaarde, 2);
        }
    }
}
