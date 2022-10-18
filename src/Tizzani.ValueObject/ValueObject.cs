namespace Tizzani.ValueObject;

public abstract record ValueObject<TValue>
{
    public TValue Value { get; }

    public ValueObject(TValue value)
    {
        Validate(value);
        Value = value;
    }

    public abstract void Validate(TValue value);

    public static implicit operator TValue(ValueObject<TValue> v) => v.Value;
}
