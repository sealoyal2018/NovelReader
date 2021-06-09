using Caliburn.Micro;
using Novel.Framework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Novel.Framework {
    public class MessageBox {
        public static async Task ShowAsync(string text) {
            var msg = IoC.Get<MessageViewModel>();
            msg.Text = text;
            await msg.ShowDialogAsync();
        }
    }
}
