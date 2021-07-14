/** Author Note: =====
* Create By: rsdte      Date: 2021-07-15 22:18:05
*/

using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Novel.Controls.TreeListView;
using Novel.Modules.Chartper;
using Novel.Modules.Chartper.Models;
using Novel.Modules.Chartper.ViewModels;
using Novel.Modules.Shell.ViewModels;
using Novel.Service;
using Novel.Service.Models;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(ActicleContentViewModel))]
	public class ActicleContentViewModel: Conductor<IChartper>, IDocument {
        
		/// <summary>
		/// 小说服务
		/// </summary>
		private readonly NovelService _service;

        private readonly DefaultCharpterViewModel _defaultCharpterViewModel;
        private TreeListViewNode root;

        /// <summary>
        /// 当前小说信息
        /// </summary>
        private NovelInfo novel;

        public TreeListViewNode Root {
            get {
                return root;
            }
            set {
                root = value;
                NotifyOfPropertyChange();
            }
        }
        
        public NovelInfo Novel {
            get {
                return novel;
            }

            set {
                novel = value;
                Root = new TreeListViewNode(value.Title);
                _defaultCharpterViewModel.Novel = novel;
                NotifyOfPropertyChange(nameof(Novel));
                NotifyOfPropertyChange(nameof(Root));
            }
        }

        [ImportingConstructor]
        public ActicleContentViewModel(NovelService service, DefaultCharpterViewModel defaultCharpterViewModel) {
            this._service = service;
            _defaultCharpterViewModel = defaultCharpterViewModel;
            ActiveItem = _defaultCharpterViewModel;
            novel = new NovelInfo();
        }

        /// <summary>
        /// 窗口被激活事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await this._service.GetCharpters(this.Novel.Href);
            ret.ForEach(x=> Root.Children.Add(new CharpterNode(x.Title, root){Href = x.Href}));
            Root.IsExpanded = true;
            await base.OnActivateAsync(cancellationToken);
            NotifyOfPropertyChange(nameof(Root));
        }

        public void ToContent(NovelCharpter charpter) {
            // var shell = IoC.Get<ShellViewModel>();
            // this._contentViewModel.Href = charpter.Href;
            // shell.ActiveItem = this._contentViewModel;
        }
    }
}