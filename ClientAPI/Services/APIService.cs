using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientAPI.Services
{

    public class APIService : IAPIService
    {
        public HttpClient Client { get; }

        public APIService(HttpClient client)
        {
            string adres = "https://localhost:44377/";
            client.BaseAddress = new Uri(adres);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
        }
    }
}
