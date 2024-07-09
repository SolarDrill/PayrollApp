namespace Payroll_Frontend.Data
{
    public interface IApiService
    {
        Task<T> GetDataAsync<T>(string endpoint);
        Task<HttpResponseMessage> PostDataAsync<T>(string endpoint, T data);
        Task<HttpResponseMessage> PutDataAsync<T>(string endpoint, T data);
        Task<HttpResponseMessage> DeleteDataAsync(string endpoint);
    }

}
