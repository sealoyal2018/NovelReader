using Caliburn.Micro;
using ICSharpCode.SharpZipLib.Zip;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Novel.Update.ViewModels {
    [Export(typeof(DownLoadViewModel))]
    public class DownLoadViewModel : Screen {
        private bool showRestartButton;
        private double progressValue;
        private readonly string _savePath;
        public FastZip fz = new();

        public bool ShowRestartButton {
            get {
                return showRestartButton;
            }

            set {
                showRestartButton = value;
                NotifyOfPropertyChange(nameof(ShowRestartButton));
            }
        }

        public double ProgressValue {
            get {
                return progressValue;
            }

            set {
                progressValue = value;
                NotifyOfPropertyChange(nameof(ProgressValue));
            }
        }


        public DownLoadViewModel() {
            _savePath = Path.Combine(Path.GetTempPath(), "noval_reader_update");
        }

        public void GetProgressValue(double value) {
            ProgressValue = value;
            Debug.WriteLine(value);
            if (value >= 100) {
                // 拷贝
                var files = App.service.FileInfos.Where(x => x.Name != "latest.json");
                var target = Directory.GetCurrentDirectory();
                foreach (var item in files) {
                    try {
                        fz.ExtractZip($"{this._savePath}\\{item.Name}", target, null);
                        Directory.Delete(_savePath, true);
                    } catch (System.Exception e) {

                    }
                }
                ShowRestartButton = true;
            }
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken) {
            base.OnActivateAsync(cancellationToken);
            return App.service.DownloadUpdateFileAsync(_savePath, GetProgressValue);
        }
    }
}
