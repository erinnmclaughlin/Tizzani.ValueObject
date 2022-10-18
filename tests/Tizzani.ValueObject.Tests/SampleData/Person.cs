namespace Tizzani.ValueObject.Tests.SampleData;

public sealed class Person
{
    public int Id { get; private set; }
    public FirstName FirstName { get; private set; }

    public Person(int id, FirstName firstName)
    {
        Id = id;
        FirstName = firstName;
    }
}
