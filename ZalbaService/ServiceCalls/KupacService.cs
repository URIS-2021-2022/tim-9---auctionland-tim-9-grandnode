
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ZalbaService.Models;

namespace ZalbaService.ServiceCalls
{
    public class KupacService : IKupacService
    {
        private readonly IConfiguration configuration;

        public KupacService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<KupacDto> GetPodnosiocaZalbe(Guid kupacId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:Kupac_SK"];
                Uri url = new Uri($"{ configuration["Services:Kupac_SK"] }api/kupac/{kupacId}");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(kupacId));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.GetAsync(url).Result;
                var responseContent = await response.Content.ReadAsStringAsync();
                var k = JsonConvert.DeserializeObject<KupacDto>(responseContent);

                return k;
            }
        }
    }
}
