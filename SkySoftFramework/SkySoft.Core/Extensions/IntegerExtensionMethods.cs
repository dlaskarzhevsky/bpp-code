
namespace SkySoft.Core
{
    /// <summary>
    /// Contains System.Integer extention methods
    /// </summary>
    public static class IntegerExtensionMethods
    {
        #region Methods
        public static int FromArray(int[] array)
        {
            int value = 0;
            int multipicator = 1;
            for (int i = array.Length - 1; i >= 3; i--)
            {
                value += array[i] * multipicator;
                multipicator *= 10;
            }

            return value;
        }
        #endregion
    }
}
