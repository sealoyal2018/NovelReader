using Caliburn.Micro;
using Novel.Update.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Update.ViewModels {
    [Export]
    public class VersionInfoViewModel: Screen {
        private VersionInfo info;

        public VersionInfo Info {
            get {
                return info;
            }

            set {
                info = value;
                NotifyOfPropertyChange(nameof(Info));
            }
        }

        public VersionInfoViewModel() {
            info = new VersionInfo {
                Content = "1.优化软件整体结构\r\n2.优化部分UI界面\r\n3.新增登录功能",
                Size = 10324d,
                Version = "0.1.3.210603",
            };
        }


        public void Updata() {
            var shell = IoC.Get<ShellViewModel>();
            shell.Content = IoC.Get<DownLoadViewModel>();
        }
    }
}
