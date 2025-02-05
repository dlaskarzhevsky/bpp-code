using System;

using SkySoft.DnsRecord.DTO;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Generates URL for DNS record
    /// </summary>
    public static class GenerateUrlForDnsRecord
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="listOfExistingDnsRecords">List of existing DNS records</param>
        /// <param name="dnsRecordDTO">DNS record</param>
        /// <param name="serverDnsRecordUrl">Server DNS record URL</param>
        public static void Execute(List<DnsRecordDTO> listOfExistingDnsRecords, DnsRecordDTO dnsRecordDTO, string serverDnsRecordUrl)
        {
            int largestUsedPortNumber = 0;
            for (int i = 0; i < listOfExistingDnsRecords.Count; i++)
            {
                int portNumber = GetPortNumberFromUrl(listOfExistingDnsRecords[i].Url);
                if (largestUsedPortNumber < portNumber)
                {
                    largestUsedPortNumber = portNumber;
                }
            }

            if (largestUsedPortNumber > 0)
            {
                largestUsedPortNumber++;
                dnsRecordDTO.Url = $"{GetProtocolTypeFromUrl(serverDnsRecordUrl)}://{dnsRecordDTO.Url}:{largestUsedPortNumber}";
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Gets port number from URL
        /// </summary>
        /// <param name="url">URL with port number</param>
        /// <returns>Port number</returns>
        static int GetPortNumberFromUrl(string? url)
        {
            int portNumber = 0;
            if (!string.IsNullOrEmpty(url))
            {
                int columnseparatorPosition = url.LastIndexOf(':');
                if (columnseparatorPosition >= 0 && columnseparatorPosition < url.Length)
                {
                    string? portNumberFromUrl = url.Substring(columnseparatorPosition + 1);
                    int.TryParse(portNumberFromUrl, out portNumber);
                }
            }

            return portNumber;
        }

        /// <summary>
        /// Gets protocol type from URL
        /// </summary>
        /// <param name="url">URL with port number</param>
        /// <returns>Protocol type</returns>
        static string GetProtocolTypeFromUrl(string? url)
        {
            string protocolType = "";
            if (!string.IsNullOrEmpty(url))
            {
                int columnseparatorPosition = url.IndexOf(':');
                if (columnseparatorPosition >= 0 && columnseparatorPosition < url.Length)
                {
                    protocolType = url.Substring(0, columnseparatorPosition);
                }
            }

            return protocolType;
        }
        #endregion
    }
}
