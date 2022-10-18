namespace Tizzani.ValueObject.Tests.SampleData;

public sealed record FirstName : ValueObject<string>
{
    public FirstName(string value) : base(value) { }

    public override void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        if (value.Length > 50)
            throw new ArgumentException("First name cannot exceed 50 characters.", nameof(value));
    }

    public static implicit operator FirstName(string value) => new(value);
}
