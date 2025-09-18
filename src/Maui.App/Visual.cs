using System.Windows.Input;

namespace Maui.App;

public static class Visual
{
    public static ICommand GetLoadedCommand(BindableObject target) => (ICommand)target.GetValue(LoadedCommandProperty);
    public static void SetLoadedCommand(BindableObject target, ICommand command) => target.SetValue(LoadedCommandProperty, command);
    public static readonly BindableProperty LoadedCommandProperty =
        BindableProperty.CreateAttached("LoadedCommand", typeof(ICommand), typeof(Visual), null, propertyChanged: (b, o, n) =>
        {
            if (n is not ICommand command)
                throw new InvalidOperationException("Command must be of type ICommand.");

            switch (b)
            {
                case Page page when !page.IsLoaded:
                    page.Loaded += Page_Loaded;
                    void Page_Loaded(object? sender, EventArgs e)
                    {
                        page.Loaded -= Page_Loaded;
                        if (command.CanExecute(null))
                            command.Execute(null);
                    }
                    break;
                case View view when !view.IsLoaded:
                    view.Loaded += View_Loaded;
                    void View_Loaded(object? sender, EventArgs e)
                    {
                        view.Loaded -= View_Loaded;
                        if (command.CanExecute(null))
                            command.Execute(null);
                    }
                    break;
                default:
                    if (command.CanExecute(null))
                        command.Execute(null);
                    break;
            }
        });
}
