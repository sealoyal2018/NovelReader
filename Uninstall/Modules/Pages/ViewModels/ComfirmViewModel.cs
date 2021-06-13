using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uninstall.Modules.Shell.ViewModels;

namespace Uninstall.Modules.Pages.ViewModels {
    [Export]
    public class ComfirmViewModel: Screen, IPage {
        

        public async Task UninstallAsync() {
            await Task.Run(() => {
                var shell = IoC.Get<ShellViewModel>();
                shell.ActiveItem = IoC.Get<UninstallViewModel>();
            });
        }
        public  async Task CancelAsync() {
            var shell = IoC.Get<ShellViewModel>();
            await shell.TryCloseAsync();
        }
    }
}
