using Caliburn.Micro;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(IDocument))]
    public class MartialArtsViewModel : Screen, IDocument {
        private readonly NovelService _service;
        private BindableCollection<NovelInfo> novels;
        
        public string Name {
            get {
                return "武侠仙侠";
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
                return 2;
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
        public MartialArtsViewModel(NovelService service) {
            novels = new BindableCollection<NovelInfo>();
            this._service = service;
        }

        /// <summary>
        /// 窗口显示事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            var ret = await _service.GetHotNovels(NovelType.Fantasy);
            this.Novels = new BindableCollection<NovelInfo>(ret);
            await base.OnActivateAsync(cancellationToken);
        }

    }
}
