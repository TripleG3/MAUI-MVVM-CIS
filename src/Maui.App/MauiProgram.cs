using Microsoft.Extensions.Logging;

namespace Maui.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.Services.AddPages().AddSpecks(options => options.AddAssemblies(
        [
            typeof(Views.MainPage).Assembly,
            typeof(ViewModels.MainPageViewModel).Assembly,
            typeof(Models.People.PersonService).Assembly
        ])).AddSingleton(sp => new NavigationPage(sp.GetService<Views.MainPage>()))
        .AddSingleton<Func<Type, Page>>(sp => type => sp.GetService(type) is Page page ? page : throw new InvalidOperationException($"Service of type {type.Name} is not a Page."));
        builder.UseMauiApp<App>().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
