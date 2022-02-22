using KomisijaService.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KomisijaService.ServiceCalls
{
    public class LicnostService : ILicnostService
    {
        private readonly IConfiguration configuration;

        public LicnostService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<LicnostDto> LicnostKomisije(Guid licnostId)
        {
            using (HttpClient client = new HttpClient())
            {
                
                Uri url = new Uri($"{ configuration["Services:LicnostService"] }api/licnost/{licnostId}");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(licnostId));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.GetAsync(url).Result;
                var responseContent = await response.Content.ReadAsStringAsync();
                var l = JsonConvert.DeserializeObject<LicnostDto>(responseContent);

                return l;
            }
        }
    }
}
