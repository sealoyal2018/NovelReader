using Novel.Framework.ViewModels;
using Novel.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Modules.Document.ViewModels {

    [Export(typeof(LoginViewModel))]
    public class LoginViewModel : DialogBase {
        private readonly NovelService service;
        private string userName;
        private string password;

        public string UserName {
            get {
                return userName;
            }

            set {
                userName = value;
                NotifyOfPropertyChange(nameof(UserName));
            }
        }

        public string Password {
            get {
                return password;
            }

            set {
                password = value;
                NotifyOfPropertyChange(nameof(Password));
            }
        }

        [ImportingConstructor]
        public LoginViewModel(NovelService service) {
            this.service = service;
        }

        public override Task InitDialogAsync() {
            this.Dialog.Height = 350d;
            this.Dialog.Width = 410d;
            this.Dialog.Content = this;
            this.Dialog.Title = "登录";
            return Task.CompletedTask;
        }

        public override async Task ConfirmAsync() {
            var ret = await this.service.Login(new Service.Models.Login { Password = Password, UserName = UserName });
            if(ret)
                await base.ConfirmAsync();

        }
    }
}
