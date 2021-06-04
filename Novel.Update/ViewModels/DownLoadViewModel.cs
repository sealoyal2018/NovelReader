using Caliburn.Micro;
using Novel.Update.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Update.ViewModels {
    [Export(typeof(DownLoadViewModel))]
    public class DownLoadViewModel: Screen {
        private DownloadInfo info;

        public DownloadInfo Info {
            get {
                return info;
            }

            set {
                info = value;
                NotifyOfPropertyChange(nameof(Info));
            }
        }


        public DownLoadViewModel() {
            info = new DownloadInfo {
                CurrentSize = 5020d,
                MaxSize = 102300d,
                FilePath = new BindableCollection<string>()
            };
        }
    }
}
