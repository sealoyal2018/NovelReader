using Caliburn.Micro;
using Novel.Commands;
using Novel.Modules.Document.ViewModels;
using Novel.Modules.Update.ViewModels;
using Novel.Service;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Novel.Modules.Shell.ViewModels {
    [Export(typeof(ShellViewModel))]
    [Export(typeof(IShell))]
    public class ShellViewModel : Conductor<IDocument>.Collection.OneActive, IShell {
        private readonly CommandBase _searchCommand;
        private bool showProgressBar;
        private bool isShowUpdateInfo;
        private readonly BindableCollection<IDocument> _documents;
        private readonly NovelService _service;

        public CommandBase SearchCommand {
            get {
                return _searchCommand;
            }
        }

        public bool ShowProgressBar {
            get {
                return showProgressBar;
            }

            set {
                showProgressBar = value;
                NotifyOfPropertyChange(nameof(ShowProgressBar));
            }
        }

        /// <summary>
        /// 显示更新信息
        /// </summary>
        public bool IsShowUpdateInfo {
            get {
                return isShowUpdateInfo;
            }

            set {
                isShowUpdateInfo = value;
            }
        }

        public BindableCollection<IDocument> Documents {
            get {
                return _documents;
            }
        }

        public NovelService Service {
            get {
                return _service;
            }
        }

        [ImportingConstructor]
        public ShellViewModel([ImportMany] IEnumerable<IDocument> documents, NovelService service) {
            DisplayName = "铅笔小说客户端";
            _documents = new BindableCollection<IDocument>(documents.OrderBy(d => d.Order));
            _searchCommand = new CommandBase(OpenSearch);
            ActiveItem = _documents.First();
            this._service = service;
        }

        public Task OnNavItemClick(RoutedPropertyChangedEventArgs<object> e) {
            if (e.NewValue is IDocument item) {
                ActiveItem = item;
            }
            return Task.CompletedTask;
        }

        public void MoveWindow() {
            Application.Current.MainWindow.DragMove();
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public void CloseWindow() {
            Application.Current.MainWindow.Close();
        }

        /// <summary>
        /// 最小化
        /// </summary>
        public void MinimizeWindow() {

            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 最大化
        /// </summary>
        public void MaximizeWindow() {
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        /// <summary>
        /// 打开搜索页面
        /// </summary>
        /// <param name="obj"></param>
        public void OpenSearch(object obj) {
            if (obj is null)
                return;
            var searchViewModel = IoC.Get<SearchViewModel>();
            searchViewModel.Keyword = obj.ToString();
            ActiveItem = searchViewModel;
        }

        public async Task SignInAsync() {
            var loginViewModel = IoC.Get<LoginViewModel>();
            var ret = await loginViewModel.ShowDialogAsync();
            NotifyOfPropertyChange(nameof(Service));
        }

        public Task SignOutAsync() {
            this.Service.SignOut();
            NotifyOfPropertyChange(nameof(Service));
            return Task.CompletedTask;
        }


        protected override async void OnViewLoaded(object view) {
            base.OnViewLoaded(view);
            if (IsShowUpdateInfo) {
                var updateView = IoC.Get<UpdateInfoViewModel>();
                await updateView.ShowDialogAsync();
            }
            /*var loginViewModel = IoC.Get<LoginViewModel>();
            var ret = await loginViewModel.ShowDialogAsync();
            if (!ret) {
                await TryCloseAsync();
            }*/
        }

    }
}
