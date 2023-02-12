using Refit;
using System.Net;

namespace CodeMasters.IntegrationTests.Factories
{
    public static class ApiExceptionFactory
    {
        public static async Task<ApiException> CreateAsync(HttpStatusCode httpStatusCode)
        {
            return await ApiException.Create("", new HttpRequestMessage(), new HttpMethod("post"), new HttpResponseMessage(httpStatusCode), new RefitSettings());
        }
    }
}
