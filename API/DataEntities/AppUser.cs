namespace API.DataEntities;
public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public byte[] PasswordHash { get; set; } = [];  // Valor por defecto
    public byte[] PasswordSalt { get; set; } = [];  // Valor por defecto
    public DateOnly BirthDay { get; set; }
    public required string KnownAs { get; set; } = "Default KnownAs";  // Valor por defecto
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastActive { get; set; } = DateTime.Now;
    public required string Gender { get; set; } = "Unknown";  // Valor por defecto
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public required string City { get; set; } = "Unknown City";  // Valor por defecto
    public required string Country { get; set; } = "Unknown Country";  // Valor por defecto
    public List<Photo> Photos { get; set; } = [];  // Valor por defecto

    // public int GetAge() => BirthDay.CalculateAge();
}