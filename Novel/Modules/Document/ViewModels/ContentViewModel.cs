using Caliburn.Micro;
using Novel.Service;
using Novel.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {
    [Export(typeof(ContentViewModel))]
    public class ContentViewModel : Screen, IDocument {
        private readonly NovelService _service;

        /// <summary>
        /// 当前小说内容
        /// </summary>
        private NovelContent novelContent;
        public string Name {
            get {
                return "小说内容";
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
                return 8;
            }
        }

        public NovelContent NovelContent {
            get {
                return novelContent;
            }

            set {
                novelContent = value;
                NotifyOfPropertyChange(nameof(NovelContent));
            }
        }

        [ImportingConstructor]
        public ContentViewModel(NovelService service) {
            this._service = service;

        }



    }
}
