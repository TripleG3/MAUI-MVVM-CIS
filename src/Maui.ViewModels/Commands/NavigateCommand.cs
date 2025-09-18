using Specky7;
using System.ComponentModel;
using System.Windows.Input;

namespace Maui.App.ViewModels.Commands;

[Transient]
public class NavigateCommand(NavigationPage navigationPage, Func<Type, Page> getPageFactory) : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public bool CanExecute(object? parameter) => parameter is Type type && type.IsSubclassOf(typeof(Page));
    public void Execute(object? parameter)
    {
        if (parameter is Type type)
        {
            var implementation = getPageFactory(type);
            if (implementation is Page page)
                _ = navigationPage.PushAsync(page);
        }
    }
}

public class BindingCommand : ICommand
{
    public event EventHandler? CanExecuteChanged;
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool> _canExecute;
    public BindingCommand(Action execute, Func<bool> canExecute, INotifyPropertyChanged notifyPropertyChanged) : this(x => execute(), x => canExecute(), notifyPropertyChanged) { }
    public BindingCommand(Action<object?> execute, Func<object?, bool> canExecute, INotifyPropertyChanged notifyPropertyChanged)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));
        notifyPropertyChanged.PropertyChanged += (s, e) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
    public bool CanExecute(object? parameter) => _canExecute(parameter);
    public void Execute(object? parameter) => _execute(parameter);
    protected virtual void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

public class BindingCommand<T>(Action<T?> execute, Func<T?, bool> canExecute, INotifyPropertyChanged notifyPropertyChanged) 
    : BindingCommand(x => execute((T?)x), x => canExecute((T?)x), notifyPropertyChanged) { }