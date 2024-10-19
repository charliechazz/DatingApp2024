using API.Entities;

namespace API.Data;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser?>> GetAllAsync();
    Task<AppUser?> GetAsync(int id);
    Task<AppUser?> GetByUsernameAsync(string username);
}