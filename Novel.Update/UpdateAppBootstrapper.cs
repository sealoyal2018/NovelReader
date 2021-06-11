using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace Novel.Update {
    public class UpdateAppBootstrapper:BootstrapperBase {
        private CompositionContainer _container;

        public UpdateAppBootstrapper() {
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
            return new DirectoryInfo(Directory.GetCurrentDirectory()).GetFiles("*.dll").Select(x => Assembly.LoadFrom(x.FullName));
        }

        protected override void OnStartup(object sender, StartupEventArgs e) {
            DisplayRootViewFor<IShell>();
        }

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
            base.OnUnhandledException(sender, e);
        }
    }
}
