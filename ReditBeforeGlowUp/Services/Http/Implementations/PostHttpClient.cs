using Domain_A1.Models;

namespace ReditBeforeGlowUp.Services.Http.Implementations;


using System.Text.Json;
using Domain_A1.DTOs;
using SharedFolder.Models;

public class PostHttpClient : IPostInterface
{
    private readonly HttpClient client;

    public PostHttpClient(HttpClient client)
    {
        this.client = client;
    }

  

    public async Task CreateAsync(PostCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/Post", dto);
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        Post post = JsonSerializer.Deserialize<Post>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
      
    }

    public async Task<ICollection<Post>> GetAllAsync()
    {
        HttpResponseMessage response = await client.GetAsync("Post/GetAllPosts");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Post> todos = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return todos;
    }

    public Task<ICollection<Post>> GetByTitleAsync(string? title)
    {
        throw new NotImplementedException();
    }
}

