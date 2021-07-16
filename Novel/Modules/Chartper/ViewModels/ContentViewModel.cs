using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using Caliburn.Micro;
using Novel.Service;
using Novel.Service.Models;

namespace Novel.Modules.Chartper.ViewModels {
    
    [Export(typeof(ContentViewModel))]
    public class ContentViewModel : Screen, IChartper, IHandle<string> {
        private string title;
        private readonly FlowDocument _document;
        private readonly NovelService _service;

        public string Title {
            get {
                return title;
            }
            set {
                title = value;
                NotifyOfPropertyChange();
            }
        }

        public FlowDocument Document => _document;
        
        
        [ImportingConstructor]
        public ContentViewModel(NovelService service) {
            this._service = service;
            _document = new FlowDocument();
            IoC.Get<IEventAggregator>().SubscribeOnPublishedThread(this);
        }

        public async Task HandleAsync(string message, CancellationToken cancellationToken) {
            var novelContent = await this._service.GetNovelContent(message);
            Title = novelContent.Title;
            Document.Blocks.Clear();
            using var sr = new StringReader(novelContent.Content);
            var line = await sr.ReadLineAsync();
            while (!string.IsNullOrEmpty(line))
            {
                var para = new Paragraph();
                var run = new Run(line);
                para.Inlines.Add(run);
                Document.Blocks.Add(para);
                line = await sr.ReadLineAsync();
            }
        }
    }
}
