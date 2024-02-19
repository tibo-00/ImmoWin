using System.ComponentModel;

namespace Odisee.Common.Observables
{
	public interface IObservableObject : INotifyPropertyChanged
	{
		bool Changed { get; set; }
	}
}