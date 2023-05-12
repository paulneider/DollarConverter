using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace DollarConverterClient;

abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Predicate<object?>? canExecute;
        private readonly Action<object?> execute;

        public Command(Action<object?> execute)
        {
            this.execute = execute;
        }
        public Command(Action<object?> execute, Predicate<object?> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }
        public void Execute(object? parameter)
        {
            execute?.Invoke(parameter);
        }
        public void NotifyCanExecutedChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
