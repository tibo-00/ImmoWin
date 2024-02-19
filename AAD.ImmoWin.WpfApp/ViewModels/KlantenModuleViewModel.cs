using AAD.ImmoWin.Business;
using AAD.ImmoWin.Business.Interfaces;
using AAD.ImmoWin.Business.Services;
using Odisee.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WpfApp.ViewModels
{
    public class KlantenModuleViewModel : BaseViewModel
	{
		#region Properties

		#region Observable properties

		private BaseViewModel _huidigeKlantenLijstViewModel;
		public BaseViewModel HuidigeKlantenLijstViewModel
		{
			get { return _huidigeKlantenLijstViewModel; }
			set
			{
				SetProperty(ref _huidigeKlantenLijstViewModel, value);
			}
		}

		private BaseViewModel _huidigeKlantDetailViewModel;
		public BaseViewModel HuidigeKlantDetailViewModel
		{
			get { return _huidigeKlantDetailViewModel; }
			set
			{
				SetProperty(ref _huidigeKlantDetailViewModel, value);
			}
		}

		private Klanten _klanten;

		public Klanten Klanten
		{
			get { return _klanten; }
			set
			{
				SetProperty(ref _klanten, value);
			}
		}

		#endregion

		#endregion

		#region Constructors

		public KlantenModuleViewModel()
		{
			// Observable properties
			Klanten = KlantenRepository.GetKlantenMetEigendommen();

            // Viewmodels
            HuidigeKlantenLijstViewModel = new KlantenLijstViewModel(Klanten);
			HuidigeKlantDetailViewModel = new KlantDetailCommandViewModel();
		}

		#endregion

		#region Methods

		#endregion
	}
}
