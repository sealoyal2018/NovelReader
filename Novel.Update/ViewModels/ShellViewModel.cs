using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace Novel.Update.ViewModels {

    [Export(typeof(IShell)), Export]
    public class ShellViewModel : Screen, IShell {
        private object content;

        public object Content {
            get {
                return content;
            }

            set {
                content = value;
                NotifyOfPropertyChange(nameof(Content));
            }
        }


        public ShellViewModel() {
            var vm =  IoC.Get<DownLoadViewModel>();
            Content = vm;
            vm.ActivateAsync();
            DisplayName = "更新";
        }
    }
}
