using Caliburn.Micro;
using NovelReaderInstaller.Modules.Pages.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReaderInstaller.Modules.Shell.ViewModels {
    [Export(typeof(IShell))]
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel: Conductor<IPage>, IShell {
        private IPage page;

        public IPage Page {
            get {
                return page;
            }

            set {
                page = value;
                NotifyOfPropertyChange(nameof(Page));
            }
        }

        public ShellViewModel() {
            Page = IoC.Get<PathViewModel>();
        }

    }
}
