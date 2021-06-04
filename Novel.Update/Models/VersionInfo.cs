using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Update.Models {
    public class VersionInfo: PropertyChangedBase {
        private string version;
        private double size;

        /// <summary>
        /// 版本次号
        /// </summary>
        public string Version {
            get {
                return version;
            }

            set {
                version = value;
                NotifyOfPropertyChange(nameof(Version));
            }
        }

        /// <summary>
        /// 下载文件的大小
        /// </summary>
        public double Size {
            get {
                return size;
            }

            set {
                size = value;
                NotifyOfPropertyChange(nameof(Size));
            }
        }

        public string Content {
            get {
                return content;
            }

            set {
                content = value;
                NotifyOfPropertyChange(nameof(Content));
            }
        }

        private string content;

    }
}
