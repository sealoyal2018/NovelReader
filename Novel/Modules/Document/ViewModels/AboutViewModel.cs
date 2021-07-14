using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(IDocument))]
    public class AboutViewModel : IDocument {

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <returns></returns>
        public Task CheckUpdateAsync() {


            return Task.CompletedTask;
        }

    }
}
