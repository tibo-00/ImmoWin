using AAD.ImmoWin.Business.Interfaces;
using Odisee.Common.Observables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.Business
{
	 public class Klanten : FullObservableCollection<IKlant>
	 {
		  public Klanten()
				: this(null)
		  {}

		  public Klanten(List<IKlant> list)
				: this((IEnumerable<IKlant>)list)
		  {}

		  public Klanten(IEnumerable<IKlant> enumerable)
				: base(enumerable)
		  {}
	 }
}
