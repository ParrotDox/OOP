using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Main
{
    class RelayCommand : ICommand
    {
        
        private Action<object?> _execute;
        private Predicate<object?> _predicate;
        public Action<object?> Exec
        { get { return _execute; } set { _execute = value; } }
        public Predicate<object?> CanExec
        { get { return _predicate; } set { _predicate = value; } }
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object?> act, Predicate<object?> check)
        {
            Exec = act;
            CanExec = check;
        }
        public bool CanExecute(object? parameter)
        {
            return CanExec(parameter);
        }

        public void Execute(object? parameter)
        {
            Exec(parameter);
        }
        public void RaiseCanExecuteChanged() 
        {
            if (CanExecuteChanged != null) 
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
