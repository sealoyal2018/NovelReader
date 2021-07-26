using Caliburn.Micro;
using Novel.Modules.Shell.ViewModels;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Novel.Modules.Chartper.ViewModels;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(SearchViewModel))]
    public class SearchViewModel : Screen, IDocument {
        
        private readonly NovelService _service;
        private readonly ContentViewModel _contentViewModel;
        private readonly string _title = "搜索";
        private readonly ActicleContentViewModel _acticleContentViewModel;
        private string keyword;
        private BindableCollection<NovelInfo> novels;

        public string Keyword {
            get {
                return keyword;
            }

            set {
                keyword = value;
                NotifyOfPropertyChange();
            }
        }

        public BindableCollection<NovelInfo> Novels {
            get {
                return novels;
            }

            set {
                novels = value;
                NotifyOfPropertyChange();
            }
        }

        public string Name {
            get {
                return _title;
            }
        }

        public string TipText {
            get {
                return _title;
            }
        }

        public int Order {
            get {
                return 20;
            }
        }

        [ImportingConstructor]
        public SearchViewModel(NovelService service, ContentViewModel contentViewModel, ActicleContentViewModel acticleContentViewModel) {
            this._service = service;
            this._contentViewModel = contentViewModel;
            _acticleContentViewModel = acticleContentViewModel;
        }

        /// <summary>
        /// 跳转到章节列表
        /// </summary>
        /// <param name="novel"></param>
        public void ToCharpterOfLeft(object obj) {
            if (obj is NovelInfo info) {
                _acticleContentViewModel.Novel = info;
                var shell = IoC.Get<ShellViewModel>();
                shell.ActiveItem = _acticleContentViewModel;
            }
        }

        /// <summary>
        /// 窗口被激活
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected async override Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await this._service.Search(Keyword);
            Novels = new BindableCollection<NovelInfo>(ret);
            await base.OnActivateAsync(cancellationToken);
        }
    }
}
