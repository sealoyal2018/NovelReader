using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(IDocument))]
    public class BeautifulViewModel : PropertyChangedBase, IDocument {
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

        public BeautifulViewModel() {
            this._name = "唯美纯爱";
            this._icon = "";
            _show = true;
        }
    }
}
