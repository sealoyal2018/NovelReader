using Caliburn.Micro;
using Novel;
using Novel.Modules.Shell.ViewModels;
using Novel.Modules.Update;
using Novel.Modules.Update.Models;
using Novel.Modules.Update.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace NovelReader {
    public class AppBootstrapper : BootstrapperBase {
        private CompositionContainer _container;
        private Task<bool> initTask;
        private readonly YSService _service;
        private readonly VersionInfo _currentVersionInfo;
        public static bool _isUpdateStart = false;
        public AppBootstrapper() {
            _service = new YSService();
            this.Initialize();
            _currentVersionInfo = new VersionInfo {
                LatestVersion = "0.2.0.210612",
                Summary = new List<string> {
                    "1.添加登录功能",
                    "2.添加更新功能",
                    "3.修复部分已知bug"
                },
                Token = Guid.NewGuid(),
                UpdateTime = DateTime.Now,
            };
        }

        protected override void Configure() {
            initTask = _service.InitServiceAsync();
            var aggregateCatalog = new AggregateCatalog();
            var allAssemblies = SelectAssemblies();
            foreach (var item in allAssemblies) {
                aggregateCatalog.Catalogs.Add(new AssemblyCatalog(item));
            }
            var batch = new CompositionBatch();
            _container = new CompositionContainer(aggregateCatalog);
            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);
            _container.Compose(batch);
            _container.ComposeParts(this);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
        }

        protected override object GetInstance(Type service, string key) {
            var contract = string.IsNullOrWhiteSpace(key) ? AttributedModelServices.GetContractName(service) : key;
            var exports = _container.GetExportedValues<object>(contract);
            if (exports.Any()) {
                return exports.First();
            }
            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }
        protected override void BuildUp(object instance) {
            _container.SatisfyImportsOnce(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies() {
            return new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("Novel*.dll").Select(x => Assembly.LoadFrom(x.FullName));
        }

        protected override async void OnStartup(object sender, StartupEventArgs e) {
            var ret = initTask.Result;
            if (ret) {
                //var info = await _service.GetLatestVersionInfoAsync();
                //if (string.Compare(info.LatestVersion, this._currentVersionInfo.LatestVersion, true) > 0) {
                //    // 启动更新软件
                //    var fileName = "Novel.Update.exe";
                //    var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
                //    var allFiles = currentDirectory.GetFiles();
                //    var file = allFiles.Where(x => x.Name == fileName).FirstOrDefault();
                //    var process = new Process {
                //        StartInfo = new ProcessStartInfo {
                //            WorkingDirectory = Directory.GetCurrentDirectory(),
                //            UseShellExecute = true,
                //            FileName = file.FullName,
                //            CreateNoWindow = true,
                //            Verb = "runas"
                //        }
                //    };
                //    process.Start();
                //    return;
                //}
            }
            var shell = IoC.Get<ShellViewModel>();
            var updateInfoViewModel = IoC.Get<UpdateInfoViewModel>();
            shell.IsShowUpdateInfo = e.Args.Length > 0;
            updateInfoViewModel.CurrentVersion = this._currentVersionInfo;
            await DisplayRootViewFor<IShell>();
        }
    }
}
