using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Odisee.Common.Observables
{
	public class ObservableObject : IObservableObject
	{
		#region Properties

		private Boolean _changed;

		public Boolean Changed
		{
			get { return _changed; }
			set
			{
				if (_changed != value)
				{
					_changed = value;
					OnPropertyChanged();
				}
			}
		}

		#endregion

		#region Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Methods

		protected virtual Boolean SetProperty<T>(ref T field, T value, [CallerMemberName] String propertyName = null)
		{
			if (!Object.Equals(field, value))
			{
				field = value;
				OnPropertyChanged(propertyName);
				Changed = true;
			}
			return Changed;
		}

		protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}


		#endregion
	}
}
