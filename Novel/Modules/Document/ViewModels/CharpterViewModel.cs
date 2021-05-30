using Caliburn.Micro;
using Novel.Modules.Shell.ViewModels;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(CharpterViewModel))]
    [Export(typeof(IDocument))]
    public class CharpterViewModel : Screen, IDocument {
        /// <summary>
        /// 小说服务
        /// </summary>
        private readonly NovelService _service;
        private readonly ContentViewModel _contentViewModel;

        /// <summary>
        /// 当前小说章节列表
        /// </summary>
        private BindableCollection<NovelCharpter> charpters;

        /// <summary>
        /// 当前小说信息
        /// </summary>
        private NovelInfo novel;

        public string Name {
            get {
                return "章节列表";
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
                return 7;
            }
        }

        public BindableCollection<NovelCharpter> Charpters {
            get {
                return charpters;
            }

            set {
                charpters = value;
                NotifyOfPropertyChange(nameof(Charpters));
            }
        }

        public NovelInfo Novel {
            get {
                return novel;
            }

            set {
                novel = value;
                NotifyOfPropertyChange(nameof(Novel));
            }
        }

        [ImportingConstructor]
        public CharpterViewModel(NovelService service, ContentViewModel contentViewModel) {
            this._service = service;
            this._contentViewModel = contentViewModel;
            novel = new NovelInfo();
            charpters = new BindableCollection<NovelCharpter>();
        }

        /// <summary>
        /// 窗口被激活事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await this._service.GetCharpters(this.Novel.Href);
            this.Charpters = new BindableCollection<NovelCharpter>(ret);
            await base.OnActivateAsync(cancellationToken);
        }

        public void ToContent(NovelCharpter charpter) {
            var shell = IoC.Get<ShellViewModel>();
            this._contentViewModel.Href = charpter.Href;
            shell.ActiveItem = this._contentViewModel;
        }

    }
}
