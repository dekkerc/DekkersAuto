using DekkersAuto.Services.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;

namespace DekkersAuto.Services.Services
{
    public class ApiService
    {
        private HttpClient _client;

        public ApiService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/");
        }


        public async Task<IEnumerable<string>> GetModelListAsync(string make)
        {
            var response = await _client.GetAsync("vehicles/GetModelsForMake/" + make +"?format=json");

            var results = await response.Content.ReadAsAsync<ResultList<ModelResult>>();

            var makes = results.Results.Select(r => r.ModelName);

            return makes;
        }

        public async Task<IEnumerable<string>> GetMakeListAsync()
        {
            var response = await _client.GetAsync("vehicles/getallmakes?format=json");

            var results = await response.Content.ReadAsAsync<ResultList<MakeResult>>();

            var makes = results.Results.Select(r => r.MakeName);

            return makes;
        }

    }
}
