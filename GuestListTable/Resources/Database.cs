using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GuestListTable.Resources
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CurrentSku
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tier")]
        public string Tier { get; set; }

        [JsonProperty("family")]
        public string Family { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }
    }

    public class DbProperties
    {
        [JsonProperty("collation")]
        public string Collation { get; set; }

        [JsonProperty("maxSizeBytes")]
        public long MaxSizeBytes { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("databaseId")]
        public string DatabaseId { get; set; }

        [JsonProperty("creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("currentServiceObjectiveName")]
        public string CurrentServiceObjectiveName { get; set; }

        [JsonProperty("requestedServiceObjectiveName")]
        public string RequestedServiceObjectiveName { get; set; }

        [JsonProperty("defaultSecondaryLocation")]
        public string DefaultSecondaryLocation { get; set; }

        [JsonProperty("catalogCollation")]
        public string CatalogCollation { get; set; }

        [JsonProperty("zoneRedundant")]
        public bool ZoneRedundant { get; set; }

        [JsonProperty("maxLogSizeBytes")]
        public long MaxLogSizeBytes { get; set; }

        [JsonProperty("earliestRestoreDate")]
        public DateTime EarliestRestoreDate { get; set; }

        [JsonProperty("readScale")]
        public string ReadScale { get; set; }

        [JsonProperty("currentSku")]
        public CurrentSku CurrentSku { get; set; }

        [JsonProperty("autoPauseDelay")]
        public int AutoPauseDelay { get; set; }

        [JsonProperty("currentBackupStorageRedundancy")]
        public string CurrentBackupStorageRedundancy { get; set; }

        [JsonProperty("requestedBackupStorageRedundancy")]
        public string RequestedBackupStorageRedundancy { get; set; }

        [JsonProperty("minCapacity")]
        public double MinCapacity { get; set; }

        [JsonProperty("maintenanceConfigurationId")]
        public string MaintenanceConfigurationId { get; set; }

        [JsonProperty("isLedgerOn")]
        public bool IsLedgerOn { get; set; }

        [JsonProperty("isInfraEncryptionEnabled")]
        public bool IsInfraEncryptionEnabled { get; set; }
    }

    public class Database
    {
        [JsonProperty("sku")]
        public Sku Sku { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("properties")]
        public DbProperties Properties { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("tags")]
        public Tags Tags { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class Sku
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tier")]
        public string Tier { get; set; }

        [JsonProperty("family")]
        public string Family { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }
    }

    public class Tags
    {
    }


}
