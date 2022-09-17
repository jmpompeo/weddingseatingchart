using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using GuestListTable.Requests;
using GuestListTable.Interfaces;

namespace GuestListTable
{
    public class GuestListTable
    {
        private static IHttpClientFactory _httpClientFactory;
        public static IDatabaseService _databaseService;
        public GuestListTable(IHttpClientFactory factory, IDatabaseService databaseService)
        {
            _httpClientFactory = factory;
            _databaseService = databaseService;
        }

        [FunctionName("GuestListTable")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            log.LogInformation("Deserializing Request");

            var reqBody = await new StreamReader(req.Body).ReadToEndAsync();
            if (string.IsNullOrWhiteSpace(reqBody))
            {
                log.LogError("Unable to properly read the stream for the incoming request body");
                throw new NullReferenceException();
            }
            var tableRequestReq = JsonConvert.DeserializeObject<TableRequestReq>(reqBody);

            if (string.IsNullOrWhiteSpace(tableRequestReq.FirstName))
            { 
                log.LogError("First Name cannot be null.");
                throw new NullReferenceException();
            }
            if (string.IsNullOrWhiteSpace(tableRequestReq.LastName))
            { 
                log.LogError("Last Name cannot be null.");
                throw new NullReferenceException();
            }

            _httpClientFactory.CreateClient();
            
        }
    }
}
