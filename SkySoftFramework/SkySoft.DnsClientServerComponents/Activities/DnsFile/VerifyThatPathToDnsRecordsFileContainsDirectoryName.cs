using System.Reflection;

namespace SkySoft.DnsClientServerComponents
{
    /// <summary>
    /// Verifies that path to DNS records file contains directory name
    /// </summary>
    public static class VerifyThatPathToDnsRecordsFileContainsDirectoryName
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <param name="pathToDnsRecordsFile">Path to DNS records file</param>
        /// <returns>Path to DNS records file with directory name</returns>
        public static string Execute(string pathToDnsRecordsFile)
        {
            string? directoryName = Path.GetDirectoryName(pathToDnsRecordsFile);
            if (string.IsNullOrEmpty(directoryName))
            {
                Assembly? assembly = Assembly.GetEntryAssembly();
                if (assembly == null)
                {
                    throw new ApplicationException("Entry assembly not found");
                }

                directoryName = Path.GetDirectoryName(assembly.Location);
            }

            pathToDnsRecordsFile = Path.Combine(directoryName!, pathToDnsRecordsFile!);

            return pathToDnsRecordsFile;
        }
        #endregion
    }
}
