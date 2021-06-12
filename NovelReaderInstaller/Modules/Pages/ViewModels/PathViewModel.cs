using Caliburn.Micro;
using NovelReaderInstaller.Modules.Shell.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReaderInstaller.Modules.Pages.ViewModels {
    [Export(typeof(PathViewModel))]
    public class PathViewModel: Screen, IPage {
        private string installPath;

        public string InstallPath {
            get {
                return installPath;
            }

            set {
                installPath = value;
                NotifyOfPropertyChange(nameof(InstallPath));
            }
        }

        public PathViewModel() {
            installPath = @"c:\programs File\NovelReader";
        }

        public Task StartInstallAsync() {
            var installViewModel = IoC.Get<InstallViewModel>();
            var shellViewModel = IoC.Get<ShellViewModel>();
            shellViewModel.Page = installViewModel;
            this.DeactivateAsync(false);
            installViewModel.ActivateAsync();
            return Task.CompletedTask;
        }

        public Task BrowerAsync() {
            var dlg = new FolderBrowserForWPF.Dialog();
            dlg.Title = "选择安装路径";
            var ret = dlg.ShowDialog();
            if (ret is true) {
                InstallPath = dlg.FileName;
            }
            return Task.CompletedTask;
        }

    }
}
