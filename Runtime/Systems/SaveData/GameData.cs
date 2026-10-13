using System;
using System.Text.Json.Serialization;
using UnityEngine;

namespace CamLib
{
    /// <summary>
    /// Derive from this class and serve as your game's master save data class. The serializer is System.Text.Json
    /// </summary>
    [Serializable]
    public abstract class GameData
    {
        [JsonPropertyName("lastUpdated")]
        public long LastUpdated;
        [JsonPropertyName("buildVersion")]
        public string BuildVersion;

        /// <summary>
        /// The values defined in this constructor will be the default values
        /// The game starts with when there's no data to load
        /// </summary>
        public virtual void OnConstruct()
        {
            BuildVersion = Application.version;
        } 
        
        /// <summary>
        /// If you update your game and accidentally introduce bugs related to save data, this can be used to repair it
        /// Potentially usable for migrating backwards as well
        /// </summary>
        public virtual void MigrateVersion(Version lastVersion, Version currentVersion) {}
    }
}
