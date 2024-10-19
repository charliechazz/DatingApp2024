using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required byte[] PasswordHash { get; set; } = [];
    public required byte[] PasswordSalt { get; set; } = [];
    public required DateOnly Birthday { get; set; }
    public required string KnownAs { get; set; }
    public required DateTime Created { get; set; }
    public required DateTime LastActive { get; set; }
    public required string Gender { get; set; }
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string? LookingFor { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public List<Photo> Photos{ get; set; } = [];

}