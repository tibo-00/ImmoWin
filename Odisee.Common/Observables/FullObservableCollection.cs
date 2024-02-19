using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odisee.Common.Observables
{
	 public class FullObservableCollection<T> : ObservableCollection<T> where T : INotifyPropertyChanged
	 {
		  public FullObservableCollection()
				: this(null)
		  {
		  }

		  public FullObservableCollection(List<T> list)
				: this ((IEnumerable<T>)list)
		  {
		  }

		  public FullObservableCollection(IEnumerable<T> enumerable)
		  {
				CollectionChanged += FullObservableCollection_CollectionChanged;
				if (enumerable != null)
				{
					 foreach(T item in enumerable)
						  Add(item);
				}
		  }

		  private void FullObservableCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		  {
				if (e.NewItems != null)
				{
					 foreach (T item in e.NewItems)
						  item.PropertyChanged += Item_PropertyChanged;
				}
				if (e.OldItems != null)
				{
					 foreach (T item in e.OldItems)
						  item.PropertyChanged -= Item_PropertyChanged;
				}
		  }

		  private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
		  {
				OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
		  }
	 }
}
