using Caliburn.Micro;
using Novel.Controls.TreeListView;
using Novel.Modules.Chartper.ViewModels;
using Novel.Modules.Document.ViewModels;

namespace Novel.Modules.Chartper.Models {
	public class CharpterNode : TreeListViewNode {

		public string Href {
			get;
			set;
		}
		public CharpterNode(string text, TreeListViewNode parent = null) : base(text, parent) {
		}

		protected override void ActiveItem() {
			base.ActiveItem();
			var view = IoC.Get<ContentViewModel>();
			var container = IoC.Get<ActicleContentViewModel>();
			view.Href = Href;
			container.ActiveItem = view;
		}
	}
}
