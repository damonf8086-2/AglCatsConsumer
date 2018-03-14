using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatsConsumer.Interfaces;
using CatsConsumer.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CatsConsumer
{
    public class PeopleService : IPeopleService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;

        public PeopleService(IConfiguration configuration, IHttpClient httpClient)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ICollection<Person>> GetPeople()
        {
            var url = _configuration["peopleUrl"];
            var result = await _httpClient.GetStringAsync(url);

            return result == null
                ? null
                : JsonConvert.DeserializeObject<List<Person>>(result);
        }
    }
}
