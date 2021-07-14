using Caliburn.Micro;
using Novel.Commands;
using Novel.Modules.Document.ViewModels;
using Novel.Modules.Update.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Novel.Modules.Shell.ViewModels {
    [Export(typeof(ShellViewModel))]
    [Export(typeof(IShell))]
    public class ShellViewModel: Conductor<IDocument>.Collection.OneActive, IShell {
        private readonly CommandBase _searchCommand;
        private bool showProgressBar;
        private bool isShowUpdateInfo;

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

        [ImportingConstructor]
        public ShellViewModel(RecommendViewModel recommendViewModel) {
            _searchCommand = new CommandBase(OpenSearch);
            ActiveItem = recommendViewModel;
            DisplayName = "铅笔小说客户端";
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
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState== WindowState.Maximized? WindowState.Normal: WindowState.Maximized;
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

        /// <summary>
        /// 导航栏切换内容
        /// </summary>
        /// <param name="e"></param>
        public void ChangedDocument(SelectionChangedEventArgs e) {
            ShowProgressBar = true;
            ActiveItem = e.AddedItems[0] as IDocument;
            ShowProgressBar = false;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            await base.OnActivateAsync(cancellationToken);
            //var loginViewModel = IoC.Get<LoginViewModel>();
            //await loginViewModel.ShowDialogAsync();
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken) {
            return base.OnInitializeAsync(cancellationToken);
        }

        protected override void OnViewReady(object view) {
            base.OnViewReady(view);
        }

        public override Task ActivateItemAsync(IDocument item, CancellationToken cancellationToken = default) {
            return base.ActivateItemAsync(item, cancellationToken);
        }
        protected override async void OnViewLoaded(object view) {
            base.OnViewLoaded(view);
            if (IsShowUpdateInfo) {
                var updateView = IoC.Get<UpdateInfoViewModel>();
                await updateView.ShowDialogAsync();
            }
            //var loginViewModel = IoC.Get<LoginViewModel>();
            //var ret = await loginViewModel.ShowDialogAsync();
            //if (!ret) {
            //    await TryCloseAsync();
            //}
        }

    }
}
