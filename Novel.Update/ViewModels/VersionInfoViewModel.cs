using Caliburn.Micro;
using Novel.Update.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Novel.Update.ViewModels {
    [Export]
    public class VersionInfoViewModel : Screen {
        private VersionInfo info;

        public VersionInfo Info {
            get {
                return info;
            }

            set {
                info = value;
                NotifyOfPropertyChange(nameof(Info));
            }
        }

        public async Task Updata() {
            var shell = IoC.Get<ShellViewModel>();
            var vm = IoC.Get<DownLoadViewModel>();
            shell.Content = vm;
            await vm.ActivateAsync();
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            Info = await App.service.GetLatestVersionInfoAsync();
            await base.OnActivateAsync(cancellationToken);
        }
    }
}
