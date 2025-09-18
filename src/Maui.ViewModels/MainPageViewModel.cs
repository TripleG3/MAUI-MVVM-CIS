using Maui.App.ViewModels.Commands;
using Specky7;

namespace Maui.App.ViewModels;

[Singleton]
public class MainPageViewModel(NavigateCommand navigateCommand)
{
    public NavigateCommand NavigateCommand { get; } = navigateCommand;
}
