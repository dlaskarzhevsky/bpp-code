using System.Reflection;

namespace SkySoft.BPPApplication
{
    /// <summary>
    /// Gets host application layer full name
    /// </summary>
    public class GetHostApplicationLayerFullName
    {
        #region Public Methods
        /// <summary>
        /// Executes activity
        /// </summary>
        /// <returns>Host application layer full name</returns>
        public static string Execute()
        {
            Assembly? assembly = Assembly.GetEntryAssembly();
            if (assembly == null)
            {
                throw new ApplicationException("Entry assembly not found");
            }

            return Path.GetFileNameWithoutExtension(assembly.Location);
        }
        #endregion
    }
}
