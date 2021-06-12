using Caliburn.Micro;
using NovelReaderInstaller.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Windows;
using System.Windows.Threading;

namespace NovelReaderInstaller {
    public class InstallerAppBootstrapper: BootstrapperBase {
        private CompositionContainer _container;

        public InstallerAppBootstrapper() {
            this.Initialize();
        }
        protected override void Configure() {
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
            return new[] { Assembly.GetExecutingAssembly() };
            //return new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("*.dll").Select(x => Assembly.LoadFrom(x.FullName));
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {
            //var identity = WindowsIdentity.GetCurrent();
            //var pincipal = new WindowsPrincipal(identity);
            //if (pincipal.IsInRole(WindowsBuiltInRole.Administrator)) {
            DisplayRootViewFor<IShell>();
            //    return;
            //}
            //var file = Assembly.GetExecutingAssembly().Location;
            //var fileName = file.Substring(0, file.LastIndexOf('.')) + ".exe";
            //var startInfo = new ProcessStartInfo {
            //    UseShellExecute = true,
            //    WorkingDirectory = Environment.CurrentDirectory,
            //    FileName = fileName,
            //    Verb = "runas",
            //};
            //Process.Start(startInfo);
            //Application.Current.Shutdown();
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
            base.OnUnhandledException(sender, e);
        }
    }
}
