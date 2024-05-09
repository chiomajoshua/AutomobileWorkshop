namespace BuildingBlocks.Infrastructure.InternetClient.Contracts;

public interface IHttpClientService
{
    Task<HttpResponseMessage> MakeHttpCall(HttpMethod httpMethod, string url, object content = null, Dictionary<string, string> customHeaders = null);
}
