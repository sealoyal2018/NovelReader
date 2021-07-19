using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(IDocument))]
    public class AboutViewModel : IDocument {
        private readonly string _title = "关于";
        public string Name {
            get {
                return _title;
            }
        }

        public string TipText {
            get {
                return _title;
            }
        }

        public int Order {
            get {
                return 100;
            }
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <returns></returns>
        public Task CheckUpdateAsync() {


            return Task.CompletedTask;
        }

    }
}
