using Tizzani.ValueObject.Tests.SampleData;

namespace Tizzani.ValueObject.Tests;

public class ValueObjectTests
{
    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(" \n \t \r ")]
    public void FirstName_ThrowsArgumentNullException_WhenValueIsNullOrWhiteSpace(string invalidValue)
    {
        Assert.Throws<ArgumentNullException>(() => _ = new FirstName(invalidValue));
    }

    [Fact]
    public void FirstName_ThrowsArgumentException_WhenValueIsTooLong()
    {
        var maxLength = 100;
        var invalidValue = string.Join("", Enumerable.Repeat("x", maxLength + 1));
        Assert.Throws<ArgumentException>(() => _ = new FirstName(invalidValue));
    }

    [Fact]
    public void FirstName_DoesNotThrowException_WhenValueIsValid()
    {
        var validValue = "Johnny";
        var firstName = new FirstName(validValue);
        Assert.Equal(validValue, firstName.Value);
    }

    [Fact]
    public void FirstNames_AreEqual_WhenValuesAreEqual()
    {
        var name1 = new FirstName("Vanessa");
        var name2 = new FirstName("Vanessa");
        Assert.Equal(name1, name2);
    }
}

