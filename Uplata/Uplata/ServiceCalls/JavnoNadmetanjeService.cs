using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Uplata.Models;

namespace Uplata.ServiceCalls
{
    public class JavnoNadmetanjeService : IJavnoNadmetanjeService
    {
        private readonly IConfiguration configuration;

        public JavnoNadmetanjeService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<JavnoNadmetanjeDto> GetJavnaNadmetanja(Guid javnoNadmetanjeID)
        {
            using (HttpClient client = new HttpClient())
            {
                
                Uri url = new Uri($"{ configuration["Services:JavnoNadmetanje"] }api/javnaNadmetanja/{javnoNadmetanjeID}");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(javnoNadmetanjeID));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.GetAsync(url).Result;
                var responseContent = await response.Content.ReadAsStringAsync();
                var j = JsonConvert.DeserializeObject<JavnoNadmetanjeDto>(responseContent);

                return j;
            }
        }
    }
}
