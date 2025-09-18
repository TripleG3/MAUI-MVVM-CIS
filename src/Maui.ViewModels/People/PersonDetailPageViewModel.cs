using Maui.App.Models.People;
using Specky7;
using System.ComponentModel;

namespace Maui.App.ViewModels.People;

[Singleton]
public class PersonDetailPageViewModel : INotifyPropertyChanged
{
    private readonly IPersonService personService;
    public PersonDetailPageViewModel(IPersonService personService)
    {
        this.personService = personService;
        personService.StateChanged += state => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public PersonServiceState State => personService.State;
}
