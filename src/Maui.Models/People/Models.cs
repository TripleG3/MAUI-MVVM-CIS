using System.Collections.Immutable;

namespace Maui.App.Models.People;

public record Person(string FirstName, string LastName, int Age, Address Address)
{
    public static Person Empty { get; } = new Person(string.Empty, string.Empty, 0, Address.Empty);
}

public record Address(string Street, string City, string State, string ZipCode)
{
    public static Address Empty { get; } = new Address(string.Empty, string.Empty, string.Empty, string.Empty);
}

public record PersonServiceState(IImmutableList<Person> People, Person SelectedPerson, bool IsLoading)
{
    public static PersonServiceState Empty { get; } = new PersonServiceState(ImmutableList<Person>.Empty, Person.Empty, false);
}