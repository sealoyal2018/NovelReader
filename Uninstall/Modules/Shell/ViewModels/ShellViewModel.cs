using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uninstall.Modules.Pages.ViewModels;

namespace Uninstall.Modules.Shell.ViewModels {
    [Export(typeof(IShell))]
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : Conductor<IPage>, IShell {
        [ImportingConstructor]
        public ShellViewModel(ComfirmViewModel comfirmViewModel) {
            ActiveItem = comfirmViewModel;
        }
    }
}
