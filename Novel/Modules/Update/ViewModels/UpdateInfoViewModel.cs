using Novel.Framework.ViewModels;
using Novel.Modules.Update.Models;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Novel.Modules.Update.ViewModels {
    [Export(typeof(UpdateInfoViewModel))]
    public class UpdateInfoViewModel : DialogBase {
        private VersionInfo currentVersion;

        public VersionInfo CurrentVersion {
            get {
                return currentVersion;
            }

            set {
                currentVersion = value;
                NotifyOfPropertyChange(nameof(CurrentVersion));
            }
        }

        public override Task InitDialogAsync() {
            this.Dialog.Title = "新版本";
            this.Dialog.Content = this;
            this.Dialog.Height = 280;
            this.Dialog.Width = 400;
            this.Dialog.ShowApplyButton = false;
            this.Dialog.ShowCancelButton = false;
            this.Dialog.ShowConfirmButton = false;
            this.Dialog.Padding = new System.Windows.Thickness(30d, 10.0, 30.0, 10.0);
            this.Dialog.ResizeMode = System.Windows.ResizeMode.NoResize;
            return Task.CompletedTask;
        }
    }
}
