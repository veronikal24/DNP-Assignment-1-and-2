using System.Text;
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

    public async Task<ICollection<Post>> GetByTitleAsync(string Title)
    {
  
        Title = Title.Replace(" ", "%20");
        HttpResponseMessage response = await client.GetAsync("/Post/GetPostByTitle?title="+Title);
       
        if (response.IsSuccessStatusCode)
        {
            // The request was successful (HTTP status code 200-299).
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the response content if it represents JSON data.
            ICollection<Post> posts = JsonSerializer.Deserialize<ICollection<Post>>(content, new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true }); 
            return posts;
        }
        else
        {
            throw new HttpRequestException($"API request failed with status code: {response.StatusCode}");
        }

        
    }



}

