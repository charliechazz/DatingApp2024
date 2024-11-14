using API.Extensions;

#pragma warning disable CA1050 // Declarar tipos en espacios de nombres
public class DateTimeExtensionsTests
#pragma warning restore CA1050 // Declarar tipos en espacios de nombres
{
    [Fact]
#pragma warning disable CA1707 // Los identificadores no deben contener caracteres de subrayado
    public void CalculateAge_ShouldReturnCorrectAge_WhenBirthdayHasPassedThisYear()
#pragma warning restore CA1707 // Los identificadores no deben contener caracteres de subrayado
    {
        // Arrange
        var birthDate = new DateOnly(2000, 1, 1); // Cumpleaños que ya pasó este año

        // Act
        var age = birthDate.CalculateAge();

        // Assert
        var expectedAge = DateTime.Now.Year - 2000;
        Assert.Equal(expectedAge, age);
    }

    [Fact]
#pragma warning disable CA1707 // Los identificadores no deben contener caracteres de subrayado
    public void CalculateAge_ShouldReturnCorrectAge_WhenBirthdayHasNotPassedThisYear()
#pragma warning restore CA1707 // Los identificadores no deben contener caracteres de subrayado
    {
        // Arrange
        var birthDate = new DateOnly(2000, DateTime.Now.Month + 1, 1); // Cumpleaños que no ha pasado este año

        // Act
        var age = birthDate.CalculateAge();

        // Assert
        var expectedAge = DateTime.Now.Year - 2000 - 1;
        Assert.Equal(expectedAge, age);
    }
}