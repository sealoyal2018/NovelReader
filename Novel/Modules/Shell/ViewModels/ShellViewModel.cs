using Caliburn.Micro;
using Novel.Commands;
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

    [Export(typeof(IShell))]
    public class ShellViewModel: Conductor<IDocument>.Collection.OneActive, IShell {

        private readonly BindableCollection<IDocument> _documents;
        private readonly CommandBase _searchCommand;

        public BindableCollection<IDocument> Documents {
            get {
                return _documents;
            }
        }

        public CommandBase SearchCommand {
            get {
                return _searchCommand;
            }
        }

        [ImportingConstructor]
        public ShellViewModel([ImportMany]IEnumerable<IDocument> documents) {
            _documents = new BindableCollection<IDocument>(documents);
            _searchCommand = new CommandBase(Search);
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
        /// 搜索
        /// </summary>
        /// <param name="obj"></param>
        public void Search(object obj) {
            if (obj is null)
                return;
        }

        /// <summary>
        /// 导航栏切换内容
        /// </summary>
        /// <param name="e"></param>
        public void ChangedDocument(SelectionChangedEventArgs e) {
            ActiveItem = e.AddedItems[0] as IDocument;
        }

    }
}
