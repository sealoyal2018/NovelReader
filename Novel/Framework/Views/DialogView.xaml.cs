using Novel.Framework.ViewModels;
using Novel.Modules.Document.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Novel.Framework.Views {
    /// <summary>
    /// DialogView.xaml 的交互逻辑
    /// </summary>
    public partial class DialogView : Window {
        public DialogView() {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            if(this.DataContext is DialogViewModel s) {
                if (Owner is null) {
                    Left = (SystemParameters.PrimaryScreenWidth - this.Width) / 2;
                    Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2;
                } else {
                    Top = Owner.Top + Math.Abs(Owner.ActualHeight - s.Height) / 2;
                    Left = Owner.Left + Math.Abs(Owner.ActualWidth - s.Width) / 2;
                }
            }
        }
    }
}
