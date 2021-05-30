using Caliburn.Micro;
using Novel.Modules.Shell.ViewModels;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {
    
    [Export(typeof(IDocument))]
    public class RomanceViewModel : Screen, IDocument {
        private readonly NovelService _service;
        private readonly ContentViewModel _contentViewModel;
        private readonly CharpterViewModel _charpterViewModel;
        private BindableCollection<NovelInfo> novels;

        public string Name {
            get {
                return "言情女生";
            }
        }

        public string Icon {
            get {
                return string.Empty;
            }
        }

        public bool Show {
            get {
                return true;
            }
        }
        public int Index {
            get {
                return 4;
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
        public RomanceViewModel(NovelService service, ContentViewModel contentViewModel, CharpterViewModel charpterViewModel) {
            novels = new BindableCollection<NovelInfo>();
            this._service = service;
            this._contentViewModel = contentViewModel;
            this._charpterViewModel = charpterViewModel;
        }
        
        /// <summary>
        /// 窗口激活事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await this._service.GetHotNovels(NovelType.Romance);
            this.Novels = new BindableCollection<NovelInfo>(ret);
            await base.OnActivateAsync(cancellationToken);
        }


        /// <summary>
        /// 跳转到章节列表
        /// </summary>
        /// <param name="novel"></param>
        public void ToCharpter(NovelInfo novel) {
            this._charpterViewModel.Novel = novel;
            var shell = IoC.Get<ShellViewModel>();
            shell.ActiveItem = this._charpterViewModel;
        }

        /// <summary>
        /// 跳转到章节内容
        /// </summary>
        /// <param name="novel"></param>
        public void ToContent(NovelInfo novel) {
            this._contentViewModel.Href = novel.LastCharpter.Href;
            var shell = IoC.Get<ShellViewModel>();
            shell.ActiveItem = this._contentViewModel;
        }
    }
}
