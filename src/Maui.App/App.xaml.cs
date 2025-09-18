namespace Maui.App;

public partial class App : Application
{
    public App(NavigationPage navigationPage)
    {
        InitializeComponent();
        NavigationPage = navigationPage;
    }

    public NavigationPage NavigationPage { get; }
    protected override Window CreateWindow(IActivationState? activationState) => new(NavigationPage);
}