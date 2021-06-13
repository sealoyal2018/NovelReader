using Caliburn.Micro;
using NovelReaderInstaller.Modules.Shell.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReaderInstaller.Modules.Pages.ViewModels {
    [Export(typeof(PathViewModel))]
    public class PathViewModel: Screen, IPage {
        private string installPath;
        private readonly string _targeDirectionName;

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
            _targeDirectionName = "NovelReader";
            installPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), _targeDirectionName);
        }

        public Task StartInstallAsync() {
            if (!InstallPath.EndsWith($"/{_targeDirectionName}")) {
                InstallPath = Path.Combine(InstallPath, _targeDirectionName);
            }
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
                if (!dlg.FileName.EndsWith($"/{_targeDirectionName}")) {
                    InstallPath = Path.Combine(dlg.FileName, _targeDirectionName);
                }
            }
            return Task.CompletedTask;
        }

    }
}
