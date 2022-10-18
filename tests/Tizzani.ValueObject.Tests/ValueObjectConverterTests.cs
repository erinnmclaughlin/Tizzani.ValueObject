using System.Text.Json;
using Tizzani.ValueObject.Json;
using Tizzani.ValueObject.Tests.SampleData;

namespace Tizzani.ValueObject.Tests;

public sealed class ValueObjectConverterTests
{
    public static JsonSerializerOptions Options
    {
        get
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ValueObjectConverter());
            return options;
        }
    }

    [Fact]
    public void CanSerializePerson()
    {
        var person = new Person(12, "Johnny");
        var expectedJson = "{\"Id\":12,\"FirstName\":\"Johnny\"}";

        var json = JsonSerializer.Serialize(person, Options);

        Assert.Equal(expectedJson, json);
    }

    [Fact]
    public void CanDeserializePerson()
    {
        var json = "{\"Id\":12,\"FirstName\":\"Johnny\"}";

        var expectedPerson = new Person(12, "Johnny");
        var person = JsonSerializer.Deserialize<Person>(json, Options)!;

        Assert.Equal(expectedPerson.Id, person.Id);
        Assert.Equal(expectedPerson.FirstName, person.FirstName);
    }
}
