#pragma warning disable IDE0161 // Convertir en namespace con ámbito de archivo
namespace API.DTOs
#pragma warning restore IDE0161 // Convertir en namespace con ámbito de archivo
{
    public class UserResponse
    {
        public required string Username { get; set; }
        public required string Token { get; set; }
    }
}