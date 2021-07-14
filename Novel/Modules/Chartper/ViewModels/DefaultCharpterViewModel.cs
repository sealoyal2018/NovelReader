/** Author Note: =====
* Create By: rsdte      Date: 2021-07-15 22:54:06
*/

using System.ComponentModel.Composition;
using Caliburn.Micro;
using Novel.Service.Models;

namespace Novel.Modules.Chartper.ViewModels {
	[Export(typeof(DefaultCharpterViewModel))]
	public class DefaultCharpterViewModel: Screen, IChartper {
		private NovelInfo novel;

		public NovelInfo Novel {
			get {
				return novel;
			}
			set {
				novel = value;
				NotifyOfPropertyChange();
			}
		}
	}
}