using System.IO;
using System.Threading;

namespace SkySoft.Core
{
    /// <summary>
    /// Contains FileInfo extention methods
    /// </summary>
    public static class FileInfoExtensionMethods
    {
        /// <summary>
        /// Used for required lock actions
        /// </summary>
        private static readonly object FileInfoLock = new object();

        #region Methods
        /// <summary>
        /// Checks if specified file is locked by another thread
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>True if file is locked, false otherwise</returns>
        public static bool IsFileLocked(this FileInfo fileInfo)
        {
            FileStream fileStream = null;
            bool result = false;
            try
            {
                fileStream = fileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // File is locked
                result = true;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if specified file size is changing
        /// </summary>
        /// <param name="fileInfo">FileInfo instance</param>
        /// <param name="waitUntillFoundFileIsReady">Flag indicating whether method need to wait until found file is ready</param>
        /// <param name="waitingTimeIncrease">Milliseconds for adding to waiting time between attempts</param>
        /// <returns>True if file size is changing, false otherwise</returns>
        private static bool IsFileSizeChanging(FileInfo fileInfo, bool waitUntillFoundFileIsReady, int waitingTimeIncrease)
        {
            bool result = false;
            long fileSize = 0;
            int intervalBetweenAttemptsInMilliseconds = 0;
            int attemptCount = 0;

            // Turn on monitoring
            bool monitor = true;
            while (monitor)
            {
                if (fileSize < fileInfo.Length)
                {
                    // File size is larger than it was at previous attempt
                    // Cache new file size
                    fileSize = fileInfo.Length;

                    if (!waitUntillFoundFileIsReady && attemptCount >= 3)
                    {
                        result = true;
                    }
                    else
                    {
                        attemptCount++;

                        // Increase interval between attempts slightly and watch file one more time
                        intervalBetweenAttemptsInMilliseconds += waitingTimeIncrease;
                        Thread.Sleep(intervalBetweenAttemptsInMilliseconds);
                        fileInfo.Refresh();
                    }
                }
                else
                {
                    // File size doesn't change. Turn off monitoring
                    monitor = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks file readiness watching file size changes
        /// </summary>
        /// <param name="fileInfo">FileInfo instance</param>
        /// <param name="waitUntillFoundFileIsReady">Flag indicating whether method need to wait until found file is ready</param>
        /// <param name="waitingTimeIncrease">Milliseconds for adding to waiting time between attempts</param>
        /// <returns>True if file is ready, otherwise False</returns>
        public static bool IsReady(this FileInfo fileInfo, bool waitUntillFoundFileIsReady = false, int waitingTimeIncrease = 10)
        {
            bool isReady = false;
            if (fileInfo.Exists)
            {
                isReady = !fileInfo.IsFileLocked();
                if (isReady)
                {
                    isReady = !IsFileSizeChanging(fileInfo, waitUntillFoundFileIsReady, waitingTimeIncrease);
                }
            }

            return isReady;
        }

        /// <summary>
        /// Moves file locking it until movement is complete
        /// </summary>
        /// <param name="fileInfo">FileInfo instance</param>
        /// <param name="throwException">Flag indicating whether occured exception during movement needs to be thrown</param>
        /// <param name="targetPath">Target path</param>
        /// <returns>True if file was moved, otherwise False</returns>
        public static bool MoveLocking(this FileInfo fileInfo, string targetPath, bool throwException = false)
        {
            lock (FileInfoLock)
            {
                if (System.IO.File.Exists(fileInfo.FullName))
                {
                    if (throwException)
                    {
                        // Occured exception during movement will be thrown
                        System.IO.File.Move(fileInfo.FullName, targetPath);
                        return true;
                    }
                    else
                    {
                        try
                        {
                            System.IO.File.Move(fileInfo.FullName, targetPath);
                            return true;
                        }
                        catch
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    // Source file was moved before this method was called
                    return false;
                }
            }
        }
        #endregion
    }
}
