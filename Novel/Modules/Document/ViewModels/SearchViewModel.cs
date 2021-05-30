using Caliburn.Micro;
using Novel.Service.Models;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(SearchViewModel))]
    public class SearchViewModel : PropertyChangedBase, IDocument {
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

    }
}
