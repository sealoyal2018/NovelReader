using Caliburn.Micro;
using Novel.Modules.Shell.ViewModels;
using Novel.Service;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(SearchViewModel))]
    public class SearchViewModel : PropertyChangedBase, IDocument {
        
        private readonly NovelService _service;
        private readonly ContentViewModel _contentViewModel;
        private readonly CharpterViewModel _charpterViewModel;
        private string keyword;

        public string Name {
            get {
                return "搜   索";
            }
        }

        public string Icon {
            get {
                return string.Empty;
            }
        }

        public bool Show {
            get {
                return false;
            }
        }

        public int Index {
            get {
                return 5;
            }
        }

        public string Keyword {
            get {
                return keyword;
            }

            set {
                keyword = value;
            }
        }

        public SearchViewModel(NovelService service, ContentViewModel contentViewModel, CharpterViewModel charpterViewModel) {
            this._service = service;
            this._contentViewModel = contentViewModel;
            this._charpterViewModel = charpterViewModel;
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
