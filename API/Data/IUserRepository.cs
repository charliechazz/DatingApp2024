using API.DTOs;
using API.DataEntities;

namespace API.Data;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>> GetAllAsync();
    Task<AppUser?> GetByIdAsync(int id);
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<IEnumerable<MemberResponse>> GetMembersAsync();
    Task<MemberResponse?> GetMemberAsync(string username);
}