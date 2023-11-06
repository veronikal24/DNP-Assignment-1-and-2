using System.ComponentModel.DataAnnotations;
using SharedFolder.Models;

using WebAPI.Services;

namespace WebApi.Services;

public class AuthService : IAuthService
{

    private readonly IList<User> users = new List<User>
    
    {
        new User
        {
           
            
            Domain = "via",
            Name = "Veronika Lietavcova",
            Password = "123",
            Role = "Student",
            Username = "Veronika",
            SecurityLevel = 4,
            Email = "Vli@via.dk",
            Age = 23,
            
        },
     
    };

    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.Username))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
  
        
        users.Add(user);
        
        return Task.CompletedTask;
    }
}