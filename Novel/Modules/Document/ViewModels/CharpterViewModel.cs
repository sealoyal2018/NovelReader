using Caliburn.Micro;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(CharpterViewModel))]
    public class CharpterViewModel : Screen, IDocument {
        /// <summary>
        /// 小说服务
        /// </summary>
        private readonly NovelService _service;

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
        public CharpterViewModel(NovelService service) {
            this._service = service;
        }

        /// <summary>
        /// 窗口被激活事件
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task OnActivateAsync(CancellationToken cancellationToken) {
            return base.OnActivateAsync(cancellationToken);
        }

    }
}
