using GuestListTable.Resources;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuestListTable.TableServices
{
    public static class DatabaseService
    {
        public static async Task<Database> GetDatabaseAsync(string dbId, HttpClient cli, ILogger log)
        {
            string getURL = $"{cli.BaseAddress.OriginalString}{dbId}?api-version=2020-08-01-preview";

            HttpRequestMessage requestMsg = new HttpRequestMessage(HttpMethod.Get, getURL);

            HttpResponseMessage responseMsg = await cli.SendAsync(requestMsg);

            if (responseMsg.IsSuccessStatusCode)
            {
                string content = await responseMsg.Content.ReadAsStringAsync();
                Database database = JsonConvert.DeserializeObject<Database>(content);

                return database;
            }
            else if (responseMsg.StatusCode == HttpStatusCode.NotFound)
            {
                log.LogError("A database could not be found with {resource id}", dbId);
                return null;
            }
            else
            {
                if (log != null)
                {
                    var msg = $"failed: {responseMsg.ReasonPhrase}";
                    log.LogWarning($"GetDatabaseAsync - {msg}.");
                }
                return null;
            }
        }
    }
}
