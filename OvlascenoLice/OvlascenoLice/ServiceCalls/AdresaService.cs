using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OvlascenoLice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OvlascenoLice.ServiceCalls
{
    public class AdresaService : IAdresaService
    {
        private readonly IConfiguration configuration;

        public AdresaService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }



        public async Task<AdresaDto> GetAdresaById(Guid AdresaId)
        {

            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:Mesto"] }api/adresa/" + AdresaId);
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
                    return JsonConvert.DeserializeObject<AdresaDto>(content);
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
