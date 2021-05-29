﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Novel.Modules.Shell.ViewModels {

    [Export(typeof(IShell))]
    public class ShellViewModel: Conductor<IDocument>, IShell {

        private readonly BindableCollection<IDocument> _documents;

        public BindableCollection<IDocument> Documents {
            get {
                return _documents;
            }
        }

        [ImportingConstructor]
        public ShellViewModel([ImportMany]IEnumerable<IDocument> documents) {
            _documents = new BindableCollection<IDocument>(documents);
        }

        public void MoveWindow() {
            Application.Current.MainWindow.DragMove();
        }

        public void CloseWindow() {
            Application.Current.MainWindow.Close();
        }

        public void MinimizeWindow() {

            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        public void MaximizeWindow() {
            Application.Current.MainWindow.WindowState = Application.Current.MainWindow.WindowState== WindowState.Maximized? WindowState.Normal: WindowState.Maximized;
        }
    }
}
