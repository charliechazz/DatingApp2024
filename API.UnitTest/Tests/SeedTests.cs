using API.Data;
using API.DataEntities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace API.UnitTest.Tests
{
    public class SeedTests
    {
        private readonly DataContext _context;

        public SeedTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            _context = new DataContext(options);
        }

        [Fact]
        public async Task SeedUsersAsync_ShouldNotSeedData_WhenUsersExist()
        {
            // Arrange: Asegúrate de que ya existen usuarios en la base de datos
            _context.Users.Add(new AppUser 
            { 
                UserName = "existingUser", 
                KnownAs = "Test", 
                Gender = "Male", 
                City = "TestCity", 
                Country = "TestCountry" 
            });
            await _context.SaveChangesAsync();

            // Act: Llama al método Seed
            await Seed.SeedUsersAsync(_context);

            // Assert: Verifica que no se hayan añadido más usuarios
            var users = await _context.Users.ToListAsync();
            Assert.Single(users); // Verifica que solo haya un usuario
        }

        [Fact]
        public async Task SeedUsersAsync_ShouldNotSeedData_WhenUserDataIsNull()
        {
            // Arrange: Simula que el archivo JSON está vacío o no tiene datos válidos
            var corruptedJsonData = "{ Invalid JSON format"; // JSON mal formado
            await File.WriteAllTextAsync("Data/UserSeedData.json", corruptedJsonData);

            // Act: Llama al método Seed
            await Seed.SeedUsersAsync(_context);

            // Assert: Verifica que no se hayan añadido usuarios
            var dbUsers = await _context.Users.ToListAsync();
            Assert.Empty(dbUsers); // No debe haber usuarios si la deserialización no produjo datos
        }

        [Fact]
        public async Task SeedUsersAsync_ShouldNotSeedData_WhenUserDataIsEmpty()
        {
            // Arrange: Crea un archivo JSON vacío
            var emptyJsonData = "[]";
            await File.WriteAllTextAsync("Data/UserSeedData.json", emptyJsonData);

            // Act: Llama al método Seed
            await Seed.SeedUsersAsync(_context);

            // Assert: Verifica que no se hayan añadido usuarios
            var dbUsers = await _context.Users.ToListAsync();
            Assert.Empty(dbUsers); // No debe haber usuarios si el JSON está vacío
        }

        [Fact]
        public async Task SeedUsersAsync_ShouldSeedData_WhenDatabaseIsEmpty()
        {
            // Arrange: Asegúrate de que la base de datos esté vacía
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            // Act: Llama al método Seed
            await Seed.SeedUsersAsync(_context);

            // Assert: Verifica que los usuarios hayan sido añadidos
            var users = await _context.Users.ToListAsync();
            Assert.NotEmpty(users); // Verifica que los usuarios fueron añadidos
        }

        [Fact]
        public async Task SeedUsersAsync_ShouldNotSeedData_WhenUserListIsNull()
        {
            // Arrange: Configura el contexto para devolver null (lo que simula el caso cuando el archivo JSON no tiene datos válidos)
            var userData = "{}"; // Simulando que el archivo JSON está vacío o no válido
            await File.WriteAllTextAsync("Data/UserSeedData.json", userData);

            // Act: Llama al método Seed
            await Seed.SeedUsersAsync(_context);

            // Assert: Verifica que no se hayan añadido usuarios
            var dbUsers = await _context.Users.ToListAsync();
            Assert.Empty(dbUsers); // No debe haber usuarios si 'users == null'
        }

        [Fact]
        public async Task SeedUsersAsync_ShouldSeedUsers_WhenUserListIsNotNull()
        {
            // Arrange: Prepara datos válidos
            var validJsonData = "[{ \"UserName\": \"testuser\", \"KnownAs\": \"Test\", \"Gender\": \"Male\", \"City\": \"TestCity\", \"Country\": \"TestCountry\" }]";
            await File.WriteAllTextAsync("Data/UserSeedData.json", validJsonData);

            // Act: Llama al método Seed
            await Seed.SeedUsersAsync(_context);

            // Assert: Verifica que se haya agregado un usuario
            var dbUsers = await _context.Users.ToListAsync();
            Assert.Single(dbUsers); // Verifica que haya un solo usuario
            Assert.Equal("testuser", dbUsers[0].UserName); // Verifica que el usuario tenga el nombre correcto
        }
    }
}