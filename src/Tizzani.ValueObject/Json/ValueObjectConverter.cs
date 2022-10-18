using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tizzani.ValueObject.Json;

public sealed class ValueObjectConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        var baseType = typeToConvert.BaseType;

        if (baseType?.IsGenericType != true)
            return false;

        if (baseType.GetGenericTypeDefinition() != typeof(ValueObject<>))
            return false;

        return true;
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var genericArguments = typeToConvert.BaseType!.GetGenericArguments();

        var valueType = genericArguments[0];

        return (JsonConverter)Activator
            .CreateInstance(typeof(ValueObjectConverterInner<,>)
            .MakeGenericType(new Type[] { valueType, typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null)!;
    }

    private sealed class ValueObjectConverterInner<TValue, TValueObject> : JsonConverter<TValueObject> where TValueObject : ValueObject<TValue>
    {
        private readonly JsonConverter<TValue> _valueConverter;
        private readonly Type _valueType;

        public ValueObjectConverterInner(JsonSerializerOptions options)
        {
            _valueConverter = (JsonConverter<TValue>)options.GetConverter(typeof(TValue));
            _valueType = typeof(TValue);
        }

        public override TValueObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = _valueConverter.Read(ref reader, _valueType, options)!;

            var valueObject = Activator.CreateInstance(typeof(TValueObject), new object[] { value });

            return (TValueObject)valueObject!;
        }

        public override void Write(Utf8JsonWriter writer, TValueObject valueObject, JsonSerializerOptions options)
        {
            var json = JsonSerializer.Serialize(valueObject.Value);
            writer.WriteRawValue(json);
        }
    }
}