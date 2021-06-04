using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Update.Models {
    public class DownloadInfo: PropertyChangedBase {
        private double maxSize;
        private double currentSize;
        private BindableCollection<string> filePath;

        public double MaxSize {
            get {
                return maxSize;
            }

            set {
                maxSize = value;
                NotifyOfPropertyChange(nameof(MaxSize));
            }
        }

        public double CurrentSize {
            get {
                return currentSize;
            }

            set {
                currentSize = value;
                NotifyOfPropertyChange(nameof(CurrentSize));
            }
        }

        public BindableCollection<string> FilePath {
            get {
                return filePath;
            }

            set {
                filePath = value;
                NotifyOfPropertyChange(nameof(FilePath));
            }
        }
    }
}
