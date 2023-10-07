using System;
using System.Net.Http;
using System.Threading.Tasks;

public class HttpClient : System.Net.Http.HttpMessageInvoker
// namespace HttpClient
{
    // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
    static readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

    static async Task Main()
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            using HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            Console.WriteLine(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }

    public HttpClient(HttpMessageHandler handler) : base(handler)
    {
    }

    public HttpClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
    {
    }
}