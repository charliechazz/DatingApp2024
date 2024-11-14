using Xunit;
using System.Collections.Generic;
using System.Linq;
using API.Helpers;
using API.DataEntities;
using API.DTOs;
using AutoMapper;
using API.Extensions;

namespace API.Tests
{
    public class AutoMapperProfileTests
    {
        private readonly IMapper _mapper;

        public AutoMapperProfileTests()
        {
            // Configuración de AutoMapper para los tests
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_AppUser_To_MemberResponse()
        {
            // Arrange: Crear un objeto AppUser
            var appUser = new AppUser
            {
                Id = 1,
                UserName = "TestUser",
                BirthDay = new DateOnly(1990, 1, 1),
                KnownAs = "Test",
                Gender = "Male",
                City = "TestCity",
                Country = "TestCountry",
                Photos = new List<Photo>
                {
                    new Photo { IsMain = true, Url = "https://example.com/photo.jpg" }
                }
            };

            // Act: Mapear AppUser a MemberResponse
            var memberResponse = _mapper.Map<MemberResponse>(appUser);

            // Assert: Verificar que las propiedades se hayan mapeado correctamente
            Assert.Equal(appUser.UserName, memberResponse.UserName);
            Assert.Equal(appUser.BirthDay.CalculateAge(), memberResponse.Age);
            Assert.Equal(appUser.Photos.FirstOrDefault(p => p.IsMain)?.Url, memberResponse.PhotoUrl);
        }

        [Fact]
        public void Should_Map_Photo_To_PhotoResponse()
        {
            // Arrange: Crear un objeto Photo
            var photo = new Photo
            {
                Id = 1,
                Url = "https://example.com/photo.jpg",
                IsMain = true
            };

            // Act: Mapear Photo a PhotoResponse
            var photoResponse = _mapper.Map<PhotoResponse>(photo);

            // Assert: Verificar que las propiedades se hayan mapeado correctamente
            Assert.Equal(photo.Id, photoResponse.Id);
            Assert.Equal(photo.Url, photoResponse.Url);
            Assert.Equal(photo.IsMain, photoResponse.IsMain);
        }

        [Fact]
        public void Should_Map_MemberUpdateRequest_To_AppUser()
        {
            // Arrange: Crear un objeto MemberUpdateRequest
            var memberUpdateRequest = new MemberUpdateRequest
            {
                Introduction = "Updated Introduction",
                Interests = "Updated Interests",
                LookingFor = "Updated LookingFor"
            };

            // Act: Mapear MemberUpdateRequest a AppUser
            var appUser = _mapper.Map<AppUser>(memberUpdateRequest);

            // Assert: Verificar que las propiedades se hayan mapeado correctamente
            Assert.Equal(memberUpdateRequest.Introduction, appUser.Introduction);
            Assert.Equal(memberUpdateRequest.Interests, appUser.Interests);
            Assert.Equal(memberUpdateRequest.LookingFor, appUser.LookingFor);
        }
    }
}
