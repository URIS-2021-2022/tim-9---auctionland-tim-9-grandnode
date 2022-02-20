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
        public bool LicnostKomisije(LicnostDto licnost)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:LicnostService"];
                Uri url = new Uri($"{ configuration["Services:LicnostService"] }api/licnost");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(licnost));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
