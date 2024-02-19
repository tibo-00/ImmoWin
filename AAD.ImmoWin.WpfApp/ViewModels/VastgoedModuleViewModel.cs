using AAD.ImmoWin.Business;
using AAD.ImmoWin.Business.Services;
using Odisee.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WpfApp.ViewModels
{
    public class VastgoedModuleViewModel : BaseViewModel
    {
        #region Properties

        #region Observable properties

        private BaseViewModel _huidigeVastgoedLijstViewModel;
        public BaseViewModel HuidigeVastgoedLijstViewModel
        {
            get { return _huidigeVastgoedLijstViewModel; }
            set
            {
                SetProperty(ref _huidigeVastgoedLijstViewModel, value);
            }
        }

        private BaseViewModel _huidigVastgoedDetailViewModel;
        public BaseViewModel HuidigVastgoedDetailViewModel
        {
            get { return _huidigVastgoedDetailViewModel; }
            set
            {
                SetProperty(ref _huidigVastgoedDetailViewModel, value);
            }
        }

        private Woningen _woningen;

        public Woningen Woningen
        {
            get { return _woningen; }
            set
            {
                SetProperty(ref _woningen, value);
            }
        }

        #endregion

        #endregion

        #region Constructors

        public VastgoedModuleViewModel()
        {
            // Observable properties
            Woningen = WoningenRepository.GetWoningen();

            // Viewmodels
            HuidigeVastgoedLijstViewModel = new VastgoedLijstViewModel(Woningen);
            HuidigVastgoedDetailViewModel = new WoningDetailCommandViewModel();
        }

        #endregion

        #region Methods

        #endregion
    }
}
