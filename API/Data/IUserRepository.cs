using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Data;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser?>> GetAllAsync();
    Task<AppUser?> GetAsync(int id);
    Task<AppUser?> GetByUsernameAsync(string username);
    Task<ActionResult<MemberResponse>> GetByIdAsync(int id);
}