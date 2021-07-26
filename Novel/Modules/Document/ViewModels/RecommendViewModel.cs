using Caliburn.Micro;
using Novel.Modules.Shell.ViewModels;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(RecommendViewModel))]
    [Export(typeof(IDocument))]
    public class RecommendViewModel : Screen, IDocument {
        private readonly NovelService _service;
        private readonly ActicleContentViewModel _acticleContentViewModel;
        private BindableCollection<NovelInfo> recommends;
        private readonly string _title = "发现";

        public BindableCollection<NovelInfo> Recommends {
            get {
                return recommends;
            }

            set {
                recommends = value;
                NotifyOfPropertyChange(nameof(Recommends));
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
                return 0;
            }
        }

        [ImportingConstructor]
        public RecommendViewModel(NovelService service, ActicleContentViewModel acticleContentViewModel) {
            recommends = new BindableCollection<NovelInfo>();
            this._service = service;
            _acticleContentViewModel = acticleContentViewModel;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await this._service.GetRecommendNovel();
            Recommends.Clear();
            ret.ForEach(x => Recommends.AddRange(x.Novels));
            await base.OnActivateAsync(cancellationToken);
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
    }
}
