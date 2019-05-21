using System;
using System.Net.Http;

namespace DekkersAuto.Services
{
    public class ApiService
    {
        private HttpClient _client;

        public ApiService(HttpClient client)
        {
            _client = client;
        }



    }
}
