using Domain_A1.Models;

namespace Application.DaoInterfaces;

public interface IUserDaoA1
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string userName);
  
}