using AAD.ImmoWin.Business.Services;
using Odisee.Common.Commands;
using Odisee.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AAD.ImmoWin.WpfApp.ViewModels
{
	public class HoofdViewModel : BaseViewModel
	{
		#region Properties

		#region Command properties

		public RelayCommand ExitCommand { get; }
		public RelayCommand KlantenModuleCommand { get; }
		public RelayCommand VastgoedModuleCommand { get; }

        public RelayCommand SeedingDbCommand { get; set; }
        public RelayCommand EmptyDbCommand { get; set; }
		public RelayCommand KeepDbCommand { get; set; }

        #endregion

        #region Observable properties

        private BaseViewModel _huidigeModuleViewModel;

		public BaseViewModel HuidigeModuleViewModel
		{
			get { return _huidigeModuleViewModel; }
			set
			{
				SetProperty(ref _huidigeModuleViewModel, value);
			}
		}

        private Visibility _popupVisibility;
        public Visibility PopupVisibility
        {
            get
            {
                return _popupVisibility;
            }
            set
            {
                _popupVisibility = value;
                OnPropertyChanged("PopupVisibility");
            }
        }

        #endregion

        #endregion

        #region Constructors

        public HoofdViewModel()
		{
			// Observable properties
			Title = "ImmoWin - MVVM - View & ViewModel";

            // Commands
            ExitCommand = new RelayCommand(ExitCommandExecute);
			KlantenModuleCommand = new RelayCommand(KlantenModuleCommandExecute, KlantenModuleCommandCanExecute);
            VastgoedModuleCommand = new RelayCommand(VastgoedModuleCommandExecute, VastgoedModuleCommandCanExecute);

            SeedingDbCommand = new RelayCommand(SeedingDbCommandExecute);
            EmptyDbCommand = new RelayCommand(EmptyDbCommandExecute);
            KeepDbCommand = new RelayCommand(KeepDbCommandExecute);
        }

        #endregion

        #region Methods

        #region Command methods
        private void SeedingDbCommandExecute()
        {
            SeedingService.DefaultDatabase();
            PopupVisibility = Visibility.Collapsed;
        }

        private void EmptyDbCommandExecute()
        {
            SeedingService.EmptyDatabase();
            PopupVisibility = Visibility.Collapsed;
        }
        private void KeepDbCommandExecute()
        {
            PopupVisibility = Visibility.Collapsed;
        }
        private void ExitCommandExecute()
		{
			Application.Current.Shutdown();
		}

        private void KlantenModuleCommandExecute()
		{
			HuidigeModuleViewModel = new KlantenModuleViewModel();
		}
		private Boolean KlantenModuleCommandCanExecute()
		{
			return !(HuidigeModuleViewModel is KlantenModuleViewModel) && PopupVisibility == Visibility.Collapsed;
        }

		private void VastgoedModuleCommandExecute()
		{
			HuidigeModuleViewModel = new VastgoedModuleViewModel();
		}

        private Boolean VastgoedModuleCommandCanExecute()
        {
            return !(HuidigeModuleViewModel is VastgoedModuleViewModel) && PopupVisibility == Visibility.Collapsed;
        }

        #endregion

        #endregion
    }
}
