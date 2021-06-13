using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Uninstall.Modules.Shell.ViewModels;

namespace Uninstall.Modules.Pages.ViewModels {
    [Export(typeof(UninstallViewModel))]
    public class UninstallViewModel: Screen, IPage {

        private double value;
        private string installPath;
        private readonly string _linkName = "NovelReader";

        public double Value {
            get {
                return value;
            }

            set {
                this.value = value;
                NotifyOfPropertyChange(nameof(Value));
            }
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken) {
            await base.OnActivateAsync(cancellationToken);
            // 删除注册表
            await DelRegeditAsync();
            Value += 2d;
            // 删除文件
            await DeleteFolderFileAsync();
            // 删除桌面快捷键
            await DeleteDesktopLinkAsync();
            Value += 2d;
            // 删除菜单快捷键
            await DeleteMenuLinkAsync();
            Value = 100d;
            var shell = IoC.Get<ShellViewModel>();
            shell.ActiveItem = IoC.Get<CompleteViewModel>();
        }

        /// <summary>
        /// 删除注册表信息
        /// </summary>
        /// <returns></returns>
        private Task DelRegeditAsync() {
            var key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            if (Environment.Is64BitOperatingSystem) {
                key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            }

            var registryKey = key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall", true);
            var uninstallKey = registryKey.OpenSubKey(_linkName);
            if (uninstallKey is null) {
                key.Close();
                return Task.CompletedTask;
            }
            if (uninstallKey.ValueCount <= 0) {
                key.DeleteSubKey(_linkName);
                key.Close();
                return Task.CompletedTask;
            }
            installPath = uninstallKey.GetValue("InstallLocation").ToString();
            registryKey.DeleteSubKey(_linkName);
            uninstallKey.Close();
            registryKey.Close();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>

        private Task DeleteFolderFileAsync() {
            var allFiles = Directory.GetFileSystemEntries(installPath);
            var name = Assembly.GetEntryAssembly().GetName().Name;
            var incretementValue = 90d / allFiles.Length;
            foreach (var file in allFiles) {
                try {
                    if (File.Exists(file)) {
                        if (file.Contains(name)) {
                            Value += incretementValue;
                            continue;
                        }
                        File.Delete(file);
                    } else {
                        Directory.Delete(file, true);
                    }
                } catch (Exception ex) {

                }
                Value += incretementValue;
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除桌面快捷键
        /// </summary>
        /// <returns></returns>
        private Task DeleteDesktopLinkAsync() {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (File.Exists(Path.Combine(desktopPath, $"{_linkName}.lnk"))) {
                File.Delete(Path.Combine(desktopPath, $"{_linkName}.lnk"));
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 删除菜单桌面快捷键
        /// </summary>
        /// <returns></returns>
        private Task DeleteMenuLinkAsync() {
            var menuPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.StartMenu);
            if (Directory.Exists(Path.Combine(menuPath, _linkName)))
                Directory.Delete(Path.Combine(menuPath, _linkName), true);
            return Task.CompletedTask;
        }
    }
}
