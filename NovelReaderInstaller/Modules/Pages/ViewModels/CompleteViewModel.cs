using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelReaderInstaller.Modules.Pages.ViewModels {
    [Export(typeof(CompleteViewModel))]
    public class CompleteViewModel : Screen, IPage {
        private readonly PathViewModel pathViewModel;

        [ImportingConstructor]
        public CompleteViewModel(PathViewModel pathViewModel) {
            this.pathViewModel = pathViewModel;
        }

        /// <summary>
        /// 启动安装的软件
        /// </summary>
        /// <returns></returns>
        public Task StartAsync() {
            var directionInfo = new DirectoryInfo(pathViewModel.InstallPath);
            var fileInfos = directionInfo.GetFiles("NovelReader.exe");
            if (fileInfos != null && fileInfos.Length > 0) {
                var process = new Process {
                    StartInfo = new ProcessStartInfo {
                        WorkingDirectory = pathViewModel.InstallPath,
                        UseShellExecute = true,
                        FileName = fileInfos[0].FullName,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
            }
            System.Windows.Application.Current.Shutdown();
            return Task.CompletedTask;
        }
    }
}
