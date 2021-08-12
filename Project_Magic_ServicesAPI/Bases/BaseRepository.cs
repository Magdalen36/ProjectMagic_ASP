using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Project_Magic_ServicesAPI.Bases
{
    public class BaseRepository
    {

        protected readonly Uri baseAddress = new Uri("http://localhost:44322/api/");
        protected readonly string route;

        public BaseRepository(string route)
        {
            this.route = route;
        }

        protected HttpClient CreateHttpClient(string token = null)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = baseAddress;
            if (token != null) client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        protected HttpResponseMessage GetResponseMessage(Func<string, Task<HttpResponseMessage>> func)
        {
            Task<HttpResponseMessage> responseTask = func(route);
            responseTask.Wait();
            return responseTask.Result;
        }
        protected HttpResponseMessage GetResponseMessage(Func<string, HttpContent, Task<HttpResponseMessage>> func, HttpContent content)
        {
            Task<HttpResponseMessage> responseTask = func(route, content);
            responseTask.Wait();
            return responseTask.Result;
        }

        protected string GetJsonContent(HttpResponseMessage response)
        {
            Task<string> content = response.Content.ReadAsStringAsync();
            content.Wait();
            return content.Result;
        }

    }
}
