namespace Maui.App.Models.People;

public interface IPersonService
{
    event Action<PersonServiceState> StateChanged;
    PersonServiceState State { get; }
    Task LoadPersonsAsync();
    void SelectPerson(Person person);
    void DeselectPerson();
}