using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Uninstall.Modules.Pages.ViewModels {
    [Export]
    public class CompleteViewModel : Screen, IPage {
        public Task CloseAsync() {
            // 删除卸载文件
            var batFileName = Path.Combine(Path.GetTempPath(), "remove.bat");
            using (var sw = new StreamWriter(batFileName)) {
                var uninstallPath = Path.Combine(Directory.GetCurrentDirectory(), $"{Assembly.GetExecutingAssembly().GetAssemblyName()}.exe");
                sw.WriteLine("cd ..");
                sw.WriteLine("ping -n 1 -w 3000 192.186.221.125");
                sw.WriteLine(string.Format("del \"{0}\" /q", uninstallPath));
            }
            //var info = new ProcessStartInfo(batFileName);
            //info.WindowStyle = ProcessWindowStyle.Hidden;
            //info.CreateNoWindow = true;
            //info.UseShellExecute = false;
            //var process = new Process() {
            //    StartInfo = info,
            //};
            //process.Start();

            var startInfo = new ProcessStartInfo {
                UseShellExecute = true,
                WorkingDirectory = Path.GetTempPath(),
                FileName = batFileName
            };
            Process.Start(startInfo);

            Application.Current.Shutdown();
            return Task.CompletedTask;
        }
    }
}
