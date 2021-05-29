using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(IDocument))]
    public class MartialArtsViewModel : PropertyChangedBase, IDocument {
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
        public MartialArtsViewModel() {
            this._name = "武侠仙侠";
            this._icon = "";
            _show = true;
        }
    }
}
