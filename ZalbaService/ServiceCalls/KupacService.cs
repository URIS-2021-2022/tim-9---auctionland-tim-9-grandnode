
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

        public KupacDto PodnosenjeZalbe(Guid kupacId)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:KupacService"];
                Uri url = new Uri($"{ configuration["Services:KupacService"] }api/kupac");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(kupacId));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    
                    return JsonConvert.DeserializeObject<KupacDto>(content.ToString());
                }
                return default;
            }
        }
    }
}
