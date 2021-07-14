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
    [Export(typeof(IDocument))]
    public class SearchViewModel : Screen, IDocument {
        
        private readonly NovelService _service;
        private readonly ContentViewModel _contentViewModel;
        private readonly ActicleContentViewModel _acticleContentViewModel;
        private string keyword;
        private BindableCollection<NovelInfo> novels;

        public string Keyword {
            get {
                return keyword;
            }

            set {
                keyword = value;
            }
        }

        public BindableCollection<NovelInfo> Novels {
            get {
                return novels;
            }

            set {
                novels = value;
                NotifyOfPropertyChange(nameof(Novels));
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
        public void ToCharpter(NovelInfo novel) {
            this._acticleContentViewModel.Novel = novel;
            var shell = IoC.Get<ShellViewModel>();
            shell.ActiveItem = this._acticleContentViewModel;
        }

        /// <summary>
        /// 跳转到章节内容
        /// </summary>
        /// <param name="novel"></param>
        public void ToContent(NovelInfo novel) {
            // this._contentViewModel.Href = novel.LastCharpter.Href;
            // var shell = IoC.Get<ShellViewModel>();
            // shell.ActiveItem = this._contentViewModel;
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
