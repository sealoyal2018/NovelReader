using Novel.Update.Services;
using System.Diagnostics;
using System.Windows;

namespace Novel.Update {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static YSService service = new YSService();
        protected override async void OnStartup(StartupEventArgs e) {
            var initTask = service.InitServiceAsync();
            var novelReader = Process.GetProcessesByName("NovelReader");
            if (novelReader != null && novelReader.Length > 0) {
                foreach (var item in novelReader) {
                    item.Kill();
                }
            }
            var ret = await initTask;
            if (!ret) {
                MessageBox.Show("升级失败!");
                this.Shutdown();
            }
            base.OnStartup(e);
        }
    }
}
