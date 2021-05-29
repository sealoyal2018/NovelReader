using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Novel.Commands {
    public class CommandBase : ICommand {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged {
            add {
                CommandManager.RequerySuggested += value;
            }
            remove {
                CommandManager.RequerySuggested -= value;
            }
        }

        public CommandBase(Action<object> execute, Func<object, bool> canExecute = null) {
            this.execute = execute;
            this.canExecute = canExecute;
        }


        public bool CanExecute(object parameter) {
            if (canExecute is null)
                return true;
            return canExecute.Invoke(parameter);
        }

        public void Execute(object parameter) {
            execute?.Invoke(parameter);
        }
    }
}
