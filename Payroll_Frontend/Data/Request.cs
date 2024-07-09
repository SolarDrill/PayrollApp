using Payroll_Frontend.Data;
using System.Text.Json;
using System.Text;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetDataAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<T>(responseStream,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })
            .ConfigureAwait(false);
    }
    public async Task<T> GetDataByIdAsync<T>(string endpoint, string id)
    {
        var response = await _httpClient.GetAsync($"{endpoint}/{id}");
        response.EnsureSuccessStatusCode();

        using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<T>(responseStream,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })
            .ConfigureAwait(false);
    }
    public async Task<HttpResponseMessage> PostDataAsync<T>(string endpoint, T data)
    {
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<HttpResponseMessage> PutDataAsync<T>(string endpoint, T data)
    {
        var jsonData = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(endpoint, content);
        response.EnsureSuccessStatusCode();

        return response;
    }

    public async Task<HttpResponseMessage> DeleteDataAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);
        response.EnsureSuccessStatusCode();

        return response;
    }
}
