using JavnoNadmetanje.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JavnoNadmetanje.ServiceCalls
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration configuration;


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="configuration"></param>
        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        /// <summary>
        /// Kreiranje poruke - metoda
        /// </summary>
        /// <param name="message"></param>
        public void CreateMessage(Message message)
        {
            using (HttpClient client = new HttpClient())
            {
                var x = configuration["Services:LoggerService"];    //Services:LoggerService je definisano u appsettings.json i sadrži lokaciju servisa
                Uri url = new Uri($"{ configuration["Services:LoggerService"]}api/logger");

                HttpContent content = new StringContent(JsonConvert.SerializeObject(message));
                content.Headers.ContentType.MediaType = "application/json";

                HttpResponseMessage response = client.PostAsync(url, content).Result;

            }
        }
    }
}
