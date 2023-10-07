namespace Domain_A1.Models;

public class Post
{
     // assumption I am making here is that each post can be 
     // only done by a registered user and you cannot see posts without having an account
     // this is usually like it is on Reddit
    public int Id { get; set; }
    public string Content { get; set; }
    public User User { get; set; }
}