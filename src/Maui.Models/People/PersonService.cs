using Specky7;
using System.Collections.Immutable;

namespace Maui.App.Models.People;

[Singleton<IPersonService>]
public class PersonService(IPersonDB personDB) : IPersonService
{
    private PersonServiceState state = PersonServiceState.Empty;

    public event Action<PersonServiceState> StateChanged = delegate { };

    public PersonServiceState State
    {
        get => state;
        private set
        {
            state = value;
            StateChanged(state);
        }
    }

    public async Task LoadPersonsAsync()
    {
        State = State with { People = ImmutableList<Person>.Empty, IsLoading = true, SelectedPerson = Person.Empty };
        State = State with { People = await personDB.GetPeopleAsync(), IsLoading = false };
    }

    public void SelectPerson(Person person)
    {
        State = State with { SelectedPerson = person };
    }

    public void DeselectPerson()
    {
        State = State with { SelectedPerson = Person.Empty };
    }
}
