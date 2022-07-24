using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TreeViewExtensionApplication_1.Common
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

    }

    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        private DelegateCommand ensure;
        private Action<Window> cancel;

        public DelegateCommand(DelegateCommand ensure)
        {
            this.ensure = ensure;
        }

        public DelegateCommand(Action<Window> cancel)
        {
            this.cancel = cancel;
        }

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }

        public virtual event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter = null)
        {
            return _canExecute == null || _canExecute();
        }

        public virtual void Execute(object parameter = null)
        {
            _execute();
        }

    }
}
