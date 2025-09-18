using Specky7;
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
