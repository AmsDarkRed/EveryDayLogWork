using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Everyday_Work.Command
{
    public class BaelViewCmd: ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            Console.WriteLine("CanExecute");
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public void Execute(object parameter)
        {
            CommandAction();
        }
    }
}
