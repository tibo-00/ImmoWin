using AAD.ImmoWin.Business.Interfaces;
using Odisee.Common.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business
{

	 public class Woningen : FullObservableCollection<IWoning>
	 {
		  public Woningen()
				: this(null)
		  { }

		  public Woningen(List<IWoning> list)
				: this((IEnumerable<IWoning>)list)
		  { }

		  public Woningen(IEnumerable<IWoning> enumerable)
				: base(enumerable)
		  { }
	 }
}
