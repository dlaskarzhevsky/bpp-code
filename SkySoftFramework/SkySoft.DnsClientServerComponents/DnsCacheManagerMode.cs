namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Defines DNS cache manager mode
    /// </summary>
    public enum DnsCacheManagerMode
    {
        /// <summary>
        /// Adds DNS record to cache
        /// </summary>
        AddDnsRecordToCache,

        /// <summary>
        /// Gets DNS record from cache by application layer name
        /// </summary>
        GetDnsRecordFromCacheByApplicationLayerName,

        /// <summary>
        /// Sets DNS records into cache
        /// </summary>
        SetDnsRecordsIntoCache
    }
}
