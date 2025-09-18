namespace Maui.App;

public static class Ioc
{
    public static Type GetBindingContext(BindableObject target) => (Type)target.GetValue(BindingContextProperty);
    public static void SetBindingContext(BindableObject target, Type type) => target.SetValue(BindingContextProperty, type);
    public static readonly BindableProperty BindingContextProperty =
        BindableProperty.CreateAttached("BindingContext", typeof(Type), typeof(Ioc), null, propertyChanged: (b, o, n) =>
        {
            if (n is not Type t)
                throw new InvalidOperationException("BindingContext must be of type Type.");

            switch (b)
            {
                case Page page when !page.IsLoaded:
                    page.Loaded += Page_Loaded;
                    void Page_Loaded(object? sender, EventArgs e)
                    {
                        page.Loaded -= Page_Loaded;
                        var instance = Application.Current?.Handler?.MauiContext?.Services.GetService(t) ?? throw new InvalidOperationException($"Service of type {t.Name} not found.");
                        page.BindingContext = instance;
                    }
                    break;
                case View view when !view.IsLoaded:
                    view.Loaded += View_Loaded;
                    void View_Loaded(object? sender, EventArgs e)
                    {
                        view.Loaded -= View_Loaded;
                        var instance = Application.Current?.Handler?.MauiContext?.Services.GetService(t) ?? throw new InvalidOperationException($"Service of type {t.Name} not found.");
                        view.BindingContext = instance;
                    }
                    break;
                default:
                    var instance = Application.Current?.Handler?.MauiContext?.Services.GetService(t) ?? throw new InvalidOperationException($"Service of type {t.Name} not found.");
                    b.BindingContext = instance;
                    break;
            }
        });
}
