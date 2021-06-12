using Caliburn.Micro;
using ICSharpCode.SharpZipLib.Zip;
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

namespace NovelReaderInstaller.Modules.Pages.ViewModels {
    [Export(typeof(InstallViewModel))]
    public class InstallViewModel : Screen, IPage {
        private double value;
        private readonly PathViewModel _pathViewModel;
        private readonly string _tempSavePath;
        private readonly string _linkName = "NovelReader";
        private readonly string _menuName = "NovelReader";
        private readonly string _programExeName = "NovelReader.exe";

        public double Value {
            get {
                return value;
            }

            set {
                this.value = value;
                NotifyOfPropertyChange(nameof(Value));
            }
        }

        [ImportingConstructor]
        public InstallViewModel(PathViewModel pathViewModel) {
            this._pathViewModel = pathViewModel;
            _tempSavePath = Path.Combine(Path.GetTempPath(), "novel_reader_temp");
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            await base.OnActivateAsync(cancellationToken);
            await StartInstallAsync();
        }

        /// <summary>
        /// 开始安装
        /// </summary>
        /// <returns></returns>
        private async Task StartInstallAsync() {
            if (Directory.Exists(_tempSavePath)) {
                Directory.Delete(_tempSavePath, true);
            }
            Directory.CreateDirectory(_tempSavePath);
            await GetEmbedFileAsync();
            await CreateDesktopShortcutAsync();
            Value += 2;
            await CreateMenuShortcutAsync();
            Value = 100;
        }


        /// <summary>
        /// 读取文件内容
        /// </summary>
        /// <returns></returns>
        private async Task GetEmbedFileAsync() {
            var assembly = Assembly.GetExecutingAssembly();
            var parentDir = "NovelReaderInstaller.Resources.Files.";
            var fileNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            var allfile = fileNames.Where(x => x.StartsWith(parentDir));
            var preMouse = 95d / allfile.Count(); // 每个压缩文件可占有的进度份额
            foreach (var fileName in allfile) {
                if (fileName.StartsWith(parentDir)) {
                    var name = fileName.Substring(parentDir.Length);
                    using (var stream = assembly.GetManifestResourceStream(fileName)) {
                        using (ZipInputStream zipStream = new ZipInputStream(stream)) {
                            ZipEntry theEntry = zipStream.GetNextEntry();
                            var zipSize = stream.Length;
                            var preStation = 1d * preMouse / zipSize; // 一个字节所占有的进度
                            while (theEntry != null) {
                                var directoryName = Path.Combine(_pathViewModel.InstallPath, Path.GetDirectoryName(theEntry.Name));
                                var filePath = Path.Combine(directoryName, Path.GetFileName(theEntry.Name));
                                if (!Directory.Exists(directoryName)) {
                                    Directory.CreateDirectory(directoryName);
                                }
                                var fileSize = theEntry.CompressedSize; // 当前文件占有的字节大小
                                if (!string.IsNullOrWhiteSpace(Path.GetFileName(theEntry.Name))) {
                                    using (FileStream streamWriter = File.Create(filePath)) {
                                        var size = 1024 * 10;
                                        var data = new byte[1024 * 10];
                                        while (true) {
                                            size = zipStream.Read(data, 0, data.Length);
                                            if (size > 0) {
                                                streamWriter.Write(data, 0, size);
                                            } else
                                                break;
                                        }
                                    }
                                }
                                Value += preStation * fileSize;
                                await Task.Delay(1);
                                theEntry = zipStream.GetNextEntry();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 创建桌面快捷键
        /// </summary>
        /// <returns></returns>
        private Task CreateDesktopShortcutAsync() {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var shellType = Type.GetTypeFromProgID("WScript.Shell");
            dynamic shell = Activator.CreateInstance(shellType);
            var shortcut = shell.CreateShortcut(Path.Combine(desktopPath, $"{_linkName}.lnk"));
            shortcut.TargetPath = Path.Combine(_pathViewModel.InstallPath, _programExeName);
            shortcut.WorkingDirectory = _pathViewModel.InstallPath;
            shortcut.Save();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 创建菜单快捷键
        /// </summary>
        /// <returns></returns>
        private Task CreateMenuShortcutAsync() {
            var startMenuPath = Environment.GetFolderPath(Environment.SpecialFolder.StartMenu);
            var shellType = Type.GetTypeFromProgID("WScript.Shell");
            dynamic shell = Activator.CreateInstance(shellType);
            var menuPath = Path.Combine(startMenuPath, _menuName);
            if (!Directory.Exists(menuPath)) {
                Directory.CreateDirectory(menuPath);
            }
            var shortcut = shell.CreateShortcut(Path.Combine(startMenuPath, $"{_linkName}.lnk"));
            shortcut.TargetPath = Path.Combine(_pathViewModel.InstallPath, _programExeName);
            shortcut.WorkingDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            shortcut.Save();
            return Task.CompletedTask;
        }

    }
}
