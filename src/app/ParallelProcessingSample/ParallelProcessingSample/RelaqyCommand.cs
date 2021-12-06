using System;
using System.Windows.Input;

namespace ParallelProcessingSample
{
    public class RelayCommand : ICommand
    {
        private Action _action;
        public RelayCommand(Action action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _action != null;
        }

        public void Execute(object parameter)
        {
            _action?.Invoke();
        }
    }
}
