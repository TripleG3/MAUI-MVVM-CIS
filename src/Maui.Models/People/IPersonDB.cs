using System.Collections.Immutable;

namespace Maui.App.Models.People
{
    public interface IPersonDB
    {
        Task<ImmutableList<Person>> GetPeopleAsync();
    }
}