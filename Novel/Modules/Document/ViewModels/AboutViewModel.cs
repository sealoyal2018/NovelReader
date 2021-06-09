using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(IDocument))]
    public class AboutViewModel : IDocument {
        public string Name {
            get {
                return "关于软件";
            }
        }

        public string Icon {
            get {
                return string.Empty;
            }
        }

        public bool Show {
            get {
                return true;
            }
        }

        public int Index {
            get {
                return 10;
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
