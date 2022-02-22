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
    public class UplataService : IUplataService
    {
        //api/uplate
        private readonly IConfiguration configuration;

        public UplataService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<UplataDTO> GetUplataById(Guid UplataID)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:ovlascenoLice"] }api/uplate/" + UplataID);
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
                    return JsonConvert.DeserializeObject<UplataDTO>(content);
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
