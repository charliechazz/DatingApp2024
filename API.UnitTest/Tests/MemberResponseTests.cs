using System;
using System.Collections.Generic;
using API.DTOs;
using Xunit;

namespace API.UnitTests.DTOs
{
    public class MemberResponseTests
    {
        [Fact]
        public void MemberResponse_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var id = 1;
            var userName = "TestUser";
            var age = 25;
            var photoUrl = "http://example.com/photo.jpg";
            var knownAs = "Test";
            var created = new DateTime(2020, 1, 1);
            var lastActive = new DateTime(2023, 1, 1);
            var gender = "Male";
            var introduction = "Hello, I'm TestUser.";
            var interests = "Coding, Hiking";
            var lookingFor = "Friendship";
            var city = "TestCity";
            var country = "TestCountry";
            var photos = new List<PhotoResponse> { new PhotoResponse { Id = 1, Url = "http://example.com/photo1.jpg" } };

            // Act
            var memberResponse = new MemberResponse
            {
                Id = id,
                UserName = userName,
                Age = age,
                PhotoUrl = photoUrl,
                KnownAs = knownAs,
                Created = created,
                LastActive = lastActive,
                Gender = gender,
                Introduction = introduction,
                Interests = interests,
                LookingFor = lookingFor,
                City = city,
                Country = country,
                Photos = photos
            };

            // Assert
            Assert.Equal(id, memberResponse.Id);
            Assert.Equal(userName, memberResponse.UserName);
            Assert.Equal(age, memberResponse.Age);
            Assert.Equal(photoUrl, memberResponse.PhotoUrl);
            Assert.Equal(knownAs, memberResponse.KnownAs);
            Assert.Equal(created, memberResponse.Created);
            Assert.Equal(lastActive, memberResponse.LastActive);
            Assert.Equal(gender, memberResponse.Gender);
            Assert.Equal(introduction, memberResponse.Introduction);
            Assert.Equal(interests, memberResponse.Interests);
            Assert.Equal(lookingFor, memberResponse.LookingFor);
            Assert.Equal(city, memberResponse.City);
            Assert.Equal(country, memberResponse.Country);
            Assert.Equal(photos, memberResponse.Photos);
        }
    }
}
