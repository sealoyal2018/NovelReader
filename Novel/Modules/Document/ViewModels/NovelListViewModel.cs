using Caliburn.Micro;
using Novel.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Novel.Modules.Document.ViewModels {
    public class NovelListViewModel : Screen {
        private BindableCollection<NovelInfo> novels;

        public BindableCollection<NovelInfo> Novels {
            get {
                return novels;
            }

            set {
                novels = value;
            }
        }

        public NovelListViewModel(IEnumerable<NovelInfo> novels) {
            novels = new BindableCollection<NovelInfo>(novels);
        }
    }
}
