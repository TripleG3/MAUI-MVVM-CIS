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
    public ICommand LoadPeopleCommand => new BindingCommand<PersonServiceState>(x => personService.LoadPersonsAsync(), x => !State.IsLoading, this);
    public ICommand SelectPersonCommand => new BindingCommand<Person>(x =>
    {
        if (x != null)
            personService.SelectPerson(x);
    }, x => !State.IsLoading && State.SelectedPerson != null, this);
    public PersonServiceState State => personService.State;
}
