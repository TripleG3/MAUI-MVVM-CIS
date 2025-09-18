using Maui.App.Models.People;
using Maui.App.ViewModels.Commands;
using Specky7;
using System.ComponentModel;
using System.Windows.Input;

namespace Maui.App.ViewModels.People;

[Singleton]
public class PersonsPageViewModel : INotifyPropertyChanged
{
    private readonly IPersonService personService;
    public PersonsPageViewModel(IPersonService personService, NavigateCommand navigateCommand)
    {
        this.personService = personService;
        NavigateCommand = navigateCommand;
        personService.StateChanged += state => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(State)));
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand NavigateCommand { get; }
    public ICommand LoadPeopleCommand => new Command(async () => await personService.LoadPersonsAsync());
    public ICommand SelectPersonCommand => new Command<Person>(personService.SelectPerson);
    public PersonServiceState State => personService.State;
}
