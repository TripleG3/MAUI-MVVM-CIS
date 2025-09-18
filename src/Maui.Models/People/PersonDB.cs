using Specky7;
using System.Collections.Immutable;

namespace Maui.App.Models.People;

[Transient<IPersonDB>]
public class PersonDB : IPersonDB
{
    private readonly List<Person> _mockDataPeople =
    [
        new Person("John", "Doe", 30, new Address("123 Main St", "Springfield", "IL", "62701")),
        new Person("Jane", "Smith", 25, new Address("456 Oak St", "Springfield", "IL", "62702")),
        new Person("Jim", "Brown", 40, new Address("789 Pine St", "Springfield", "IL", "62703"))
    ];

    public async Task<ImmutableList<Person>> GetPeopleAsync()
    {
        await Task.Delay(2000); // Simulate database access delay
        return [.. _mockDataPeople];
    }
}
