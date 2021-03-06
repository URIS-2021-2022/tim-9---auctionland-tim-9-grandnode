using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UgovorService.Models;

namespace UgovorService.ServiceCalls
{
    public class KupacSkService : IKupacSkService
    {

        private readonly IConfiguration configuration;

        public KupacSkService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<KupacDto> GetKupacById(Guid kupacId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:Kupac_SK"] }api/kupci/" + kupacId);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }
                    return JsonConvert.DeserializeObject<KupacDto>(content);
                }
                return default;
            }
            catch
            {
                return default;
            }
        }
    }
}
