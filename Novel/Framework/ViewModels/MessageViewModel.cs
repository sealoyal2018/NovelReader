using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Framework.ViewModels {
    [Export]
    public class MessageViewModel : DialogBase {
        private string text;

        public string Text {
            get {
                return text;
            }

            set {
                text = value;
                NotifyOfPropertyChange(nameof(Text));
            }
        }

        public override Task InitDialogAsync() {
            this.Dialog.Content = this;
            this.Dialog.ShowCancelButton = false;
            this.Dialog.ShowConfirmButton = false;
            this.Dialog.Title = "提示";
            this.Dialog.ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
            this.Dialog.Height = 245d;
            this.Dialog.Width = 360d;
            return Task.CompletedTask;
        }
    }
}
