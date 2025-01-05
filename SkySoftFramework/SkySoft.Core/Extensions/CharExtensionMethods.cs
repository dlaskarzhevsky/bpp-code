namespace SkySoft.Core
{
    /// <summary>
    /// Contains System.Char extention methods
    /// </summary>
    public static class CharExtensionMethods
    {
        #region Methods
        /// <summary>
        /// Verifies whether provided characters contain current character 
        /// </summary>
        /// <param name="value">Current character</param>
        /// <param name="characters">Provided characters</param>
        /// <returns>True if provided characters contain current character, otherwise False</returns>
        public static bool ContainsIn(this char value, char[] characters)
        {
            for (int i = 0; i < characters.Length; i++)
            {
                if (value == characters[i])
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }
}
