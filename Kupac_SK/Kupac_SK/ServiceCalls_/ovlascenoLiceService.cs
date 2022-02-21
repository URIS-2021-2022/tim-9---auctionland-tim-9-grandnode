using Kupac_SK.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Kupac_SK.ServiceCalls_
{
    public class ovlascenoliceService : IovlascenoliceService
    {


        private readonly IConfiguration configuration;

        public ovlascenoliceService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<OvlascenoLiceDTO> GetOvlascenoLiceById(Guid OLiceID)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:ovlascenoLice"] }api/ovlascenaLica/" + OLiceID);
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
                    return JsonConvert.DeserializeObject<OvlascenoLiceDTO>(content);
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
