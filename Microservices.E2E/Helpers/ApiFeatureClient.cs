using System.Net.Http.Json;
using Newtonsoft.Json;

namespace Microservices.E2E.Helpers;

public abstract class ApiFeatureClient
{
    protected string Host { get; }
    protected int Port { get; }
    protected bool UseSsl { get; }

    protected string Endpoint => $"http{(UseSsl ? "s" : "")}://{Host}:{Port}/";

    protected HttpClient _httpClient;
    
    protected Uri Uri { get; }
    
    private ApiFeatureClient(string host, int port, bool useSsl = false)
    {
        Host = host;
        Port = port;
        UseSsl = useSsl;
        Uri = new Uri(Endpoint);
        
        _httpClient = new HttpClient()
        {
            BaseAddress = Uri
        };
    }

    public async Task<TResult?> GetAsync<TResult>(string path)
    {
        var response = await _httpClient.GetAsync(path);
        
        response.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync());
    }
    
    public async Task<TResult?> PostAsync<TRequest, TResult>(string path, TRequest body)
    {
        var response = await _httpClient.PostAsync(path, JsonContent.Create(body));
        
        response.EnsureSuccessStatusCode();

        return JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync());
    }
    
}