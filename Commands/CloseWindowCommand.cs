using System;
using System.Windows;
using System.Windows.Input;

namespace MemoryAnalyzer.Commands
{
    public class CloseWindowCommand : ICommand
    {
        private CloseWindowCommand()
        {
            
        }

        public bool CanExecute(object parameter)
        {
            return parameter is Window;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                ((Window)parameter).Close();
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { throw new NotSupportedException(); }
            remove { }
        }

        public static readonly ICommand Instance = new CloseWindowCommand();
    }
}