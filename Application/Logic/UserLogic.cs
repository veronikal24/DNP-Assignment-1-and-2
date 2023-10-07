using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain_A1.DTOs;
using Domain_A1.Models;

namespace Application.Logic;

public class UserLogic: IUserLogic
{
    private readonly IUserDaoA1 _userDaoA1;

    public UserLogic(IUserDaoA1 userDaoA1)
    {
        this._userDaoA1 = userDaoA1;
    }

    public async Task<User> CreateAsync(UserCreationDto userToCreate)
    { 
        User? existing = await _userDaoA1.GetByUsernameAsync(userToCreate.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(userToCreate);
        User toCreate = new User
        {
            UserName = userToCreate.UserName,
            Password = userToCreate.Password
        };
      
    
        User created = await _userDaoA1.CreateAsync(toCreate);
    
        return created;
        
    }
    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;

        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 30)
            throw new Exception("Username must be less than 30 characters!");
     
    }
}