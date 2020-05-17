using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientAPI.Services
{
    public class API_Service : IAPIService
    {
        public HttpClient Client { get; }

        public API_Service(HttpClient client)
        {
            string addressURL = "https://localhost:5001/";
            client.BaseAddress = new Uri(addressURL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client = client;
        }
    }
}
