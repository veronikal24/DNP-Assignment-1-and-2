namespace Domain_A1.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    
    public User(string username, string password)
    {
        UserName = username;
        Password = password;
    }
    
    
}