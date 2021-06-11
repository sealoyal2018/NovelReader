using Novel.Update.Services;
using System.Windows;

namespace Novel.Update {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static YSService service = new YSService();
        protected override async void OnStartup(StartupEventArgs e) {
            var ret = await service.InitServiceAsync();
            if (!ret) {
                MessageBox.Show("升级失败!");
                this.Shutdown();
            }
            base.OnStartup(e);
        }
    }
}
