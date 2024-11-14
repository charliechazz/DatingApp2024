using System;
using Xunit;
using API.DataEntities;

public class PhotoTests
{
    [Fact]
public void Photo_ShouldInitializePropertiesCorrectly()
{
    // Arrange
    var appUser = new AppUser 
    { 
        Id = 42, 
        UserName = "Test User", 
        KnownAs = "Test",  // Proporcionar valores requeridos
        Gender = "Male",   // Proporcionar valores requeridos
        City = "Test City", // Proporcionar valores requeridos
        Country = "Test Country" // Proporcionar valores requeridos
    };
    
    var photo = new Photo
    {
        Id = 1,
        Url = "https://example.com/photo.jpg",
        IsMain = true,
        PublicId = "public-id-123",
        AppUserId = 42,
        AppUser = appUser
    };

    // Act & Assert
    Assert.Equal(1, photo.Id);
    Assert.Equal("https://example.com/photo.jpg", photo.Url);
    Assert.True(photo.IsMain);
    Assert.Equal("public-id-123", photo.PublicId);
    Assert.Equal(42, photo.AppUserId);
    Assert.NotNull(photo.AppUser);
    Assert.Equal(42, photo.AppUser.Id);
    Assert.Equal("Test User", photo.AppUser.UserName);
    Assert.Equal("Test", photo.AppUser.KnownAs);
    Assert.Equal("Male", photo.AppUser.Gender);
    Assert.Equal("Test City", photo.AppUser.City);
    Assert.Equal("Test Country", photo.AppUser.Country);
}
}
