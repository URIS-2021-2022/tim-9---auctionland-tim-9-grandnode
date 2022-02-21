using Microsoft.AspNetCore.Mvc;
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
    public class Dokument_AKService : IDokument_AKService
    {

        private readonly IConfiguration configuration;

        public Dokument_AKService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<DokumentDto> GetDokumentByID(Guid dokumentID)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{ configuration["Services:Dokument_AK"] }api/dokument/" + dokumentID);
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
                    return JsonConvert.DeserializeObject<DokumentDto>(content);
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
