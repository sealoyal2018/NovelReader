using Caliburn.Micro;
using Novel.Controls.TreeListView;
using Novel.Modules.Chartper.ViewModels;
using Novel.Modules.Document.ViewModels;

namespace Novel.Modules.Chartper.Models {
	public class CharpterNode : TreeListViewNode {
		private readonly IEventAggregator _event;

		public string Href {
			get;
			set;
		}
		public CharpterNode(string text, TreeListViewNode parent = null) : base(text, parent) {
			_event = IoC.Get<IEventAggregator>();
		}

		protected override async void ActiveItem() {
			base.ActiveItem();
			var view = IoC.Get<ContentViewModel>();
			var container = IoC.Get<ActicleContentViewModel>();
			await _event.PublishOnCurrentThreadAsync(Href);
			if(container.ActiveItem != view)
				container.ActiveItem = view;
		}
	}
}
