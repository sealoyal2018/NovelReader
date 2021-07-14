using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Novel.Service;
using Novel.Service.Models;

namespace Novel.Modules.Chartper.ViewModels {
    
    [Export(typeof(ContentViewModel))]
    public class ContentViewModel : Screen, IChartper {
        private readonly NovelService _service;
        private string href;

        /// <summary>
        /// 当前小说内容
        /// </summary>
        private NovelContent novelContent;

        public NovelContent NovelContent {
            get {
                return novelContent;
            }

            set {
                novelContent = value;
                NotifyOfPropertyChange(nameof(NovelContent));
            }
        }

        public string Href {
            get {
                return href;
            }

            set {
                href = value;
            }
        }

        [ImportingConstructor]
        public ContentViewModel(NovelService service) {
            this._service = service;
            novelContent = new NovelContent();
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            NovelContent = await this._service.GetNovelContent(Href);
            await base.OnActivateAsync(cancellationToken);
        }

    }
}
