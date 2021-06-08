using Caliburn.Micro;
using System.Threading.Tasks;

namespace Novel.Framework.ViewModels {
    public abstract class DialogBase : Screen {
        private readonly DialogViewModel _dialog;
        private readonly IWindowManager _windowManager;

        public DialogViewModel Dialog {
            get {
                return _dialog;
            }
        }

        public DialogBase() {
            this._dialog = new DialogViewModel();
            this._windowManager = IoC.Get<IWindowManager>();
        }

        public virtual Task ConfirmAsync() {
            return _dialog.TryCloseAsync(true);
        }

        public virtual Task CancelAsync() {
            return _dialog.TryCloseAsync(false);
        }

        public virtual Task ApplyAsync() {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 对对话框进行配置
        /// </summary>
        /// <returns></returns>
        public abstract Task InitDialogAsync();

        /// <summary>
        /// 弹出对话框
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ShowDialogAsync() {
            await InitDialogAsync();
            var ret = await _windowManager.ShowDialogAsync(Dialog);
            if (ret is bool b && b) {
                return b;
            }
            return false;
        }
    }
}
