using GuestListTable.Resources;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace GuestListTable.Interfaces
{
    public interface IDatabaseService
    {
        public Task<Database> GetDatabaseAsync(string dbId, HttpClient cli, ILogger log);
        public DataTable GetDbResults(string query, string serverName, string dbName, string token, ILogger log);
    }
}
