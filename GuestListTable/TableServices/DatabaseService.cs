using GuestListTable.Resources;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GuestListTable.Interfaces;

namespace GuestListTable.TableServices
{
    public class DatabaseService
    {
        public async Task<Database> GetDatabaseAsync(string dbId, HttpClient cli, ILogger log)
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

        internal string GetConnectionString(string serverName, string dbName, string token, ILogger log)
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = serverName,
                InitialCatalog = dbName,
                ConnectTimeout = 60,
                Encrypt = true,
                TrustServerCertificate = false,
                MultipleActiveResultSets = false,
                ConnectRetryCount = 3,
                ConnectRetryInterval = 15                
            };

            return builder.ToString();
        }

        internal SqlConnection GetSqlConnection(string serverName, string dbName, string token, ILogger log)
        {
            var connectionString = GetConnectionString(serverName, dbName, token, log);

            var connection = new SqlConnection(connectionString);

            var counter = 0;
            var attempts = counter - 1;

            try
            {
                do
                {
                    try
                    {
                        connection.Open();
                        return connection;
                    }
                    catch (Exception ex)
                    {
                        counter++;
                        log.LogError(ex, "Failed to connect to SQL. Retrying {attempts} more times.", attempts);
                        throw;
                    }
                } while (counter < 5 && connection.State != ConnectionState.Open);
            }
            catch (Exception e)
            {
                log.LogError(e, "SQL connection failed & maximum number of retries have been attempted.");
                throw;
            }
        }

        public DataTable GetDbResults(string query, string serverName, string dbName, string token, ILogger log)
        {
            var results = new DataTable();
            try
            {
                var sqlConnection = GetSqlConnection(serverName, dbName, token, log);
                using SqlCommand command = new(query, sqlConnection);
                using SqlDataAdapter adapter = new(command);
                adapter.Fill(results);
                sqlConnection.Dispose();
            }
            catch (Exception e)
            {
                log.LogError(e, "Failed to get db results");
                return null;
            }

            return results;
        }
    }
}
