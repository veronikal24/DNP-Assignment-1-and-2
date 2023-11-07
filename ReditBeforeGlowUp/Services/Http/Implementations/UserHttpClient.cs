using System.Text;
using System.Text.Json;
using Application.Logic;
using Application.LogicInterfaces;
using Domain_A1.DTOs;
using SharedFolder.Models;

namespace ReditBeforeGlowUp.Services.Http.Implementations;

public class UserHttpClient : IUserService
{
    private readonly HttpClient client;
    private readonly IUserLogic _userLogic;

    public UserHttpClient(HttpClient client)
    {
        this.client = client;
        client.Timeout = TimeSpan.FromSeconds(30);
    }
    
    public async  Task CreateAsync(UserCreationDto dto)
    {
        
            HttpResponseMessage response = await client.PostAsJsonAsync("/Users", dto);
            string result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Could not register the users" + dto.UserName );
                throw new Exception(result);
            }

            User user = JsonSerializer.Deserialize<User>(result)!;
            Console.WriteLine("Registered User:" + user.Username );
        

    }

    public async Task<ICollection<Domain_A1.Models.User>> GetUserByNameAsync(string? name)
    {
       
        HttpResponseMessage response = await client.GetAsync("/Users?username="+name);
       
        if (response.IsSuccessStatusCode)
        {
            // The request was successful (HTTP status code 200-299).
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the response content if it represents JSON data.
            ICollection<Domain_A1.Models.User> posts = JsonSerializer.Deserialize<ICollection<Domain_A1.Models.User>>(content, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true });
            return posts;
        }
        else
        {
            throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
        }
    }
}
