namespace Maui.App;

public static class MauiExtensions
{
    public static IServiceCollection AddPages(this IServiceCollection services)
    {
        var pageTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(Page).IsAssignableFrom(type) && !type.IsAbstract && type.IsPublic);
        foreach (var pageType in pageTypes)
        {
            services.AddTransient(pageType);
        }
        return services;
    }
}
