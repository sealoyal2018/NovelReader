using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(SearchViewModel))]
    public class SearchViewModel : PropertyChangedBase, IDocument {
        private readonly string _name;
        private readonly string _icon;
        private readonly bool _show;

        public string Name {
            get {
                return _name;
            }
        }
        public string Icon {
            get {
                return _icon;
            }
        }
        public bool Show {
            get {
                return _show;
            }
        }
         

        public SearchViewModel() {
            this._name = "搜索";
            this._icon = "";
            _show = false;
        }


    }
}
