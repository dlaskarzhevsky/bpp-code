using System;
using System.Globalization;

namespace SkySoft.Core
{
    /// <summary>
    /// Creates object
    /// </summary>
    public static class ObjectCreator
    {
        #region Properties
        /// <summary>
        /// Holds path to root folder
        /// </summary>
        public static string? PathToRootFolder
        {
            get; set;
        }
        #endregion

        #region methods
        /// <summary>
        /// Creates object of specified type
        /// </summary>
        /// <param name="objectTypeName">Object type full name (required)</param>
        /// <param name="throwTypeNotFoundException">Flag indicating whether exception needs to be thrown if type not found</param>
        /// <param name="args">Constructor arguments</param>
        /// <returns>Object of requested type</returns>
        public static object? Create(string objectTypeName, bool throwTypeNotFoundException = true, params object[] args)
        {
            // Validate arguments
            if (string.IsNullOrEmpty(objectTypeName))
            {
                throw new ArgumentNullException("objectTypeName");
            }

            Type? type = GetType(objectTypeName);
            if (type == null)
            {
                if (throwTypeNotFoundException)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, SkySoft.Core.Resources.TypeNotFound, objectTypeName));
                }
                else
                {
                    return null;
                }
            }
            else
            {
                object? instance = Activator.CreateInstance(type, args);
                return instance;
            }
        }

        /// <summary>
        /// Gets type
        /// </summary>
        /// <param name="typeFullName">Type full name (required)</param>
        /// <returns>Object of requested type</returns>
        public static Type? GetType(string typeFullName)
        {
            // Validate arguments
            if (string.IsNullOrEmpty(typeFullName))
            {
                throw new ArgumentNullException("objectTypeName");
            }

            Type? type = null;
            if (typeFullName.Contains(","))
            {
                type = Type.GetType(typeFullName);
            }
            else
            {
                type = ObjectCreator.GetTypeUsingPartialName(typeFullName);
            }

            if (type == null && !string.IsNullOrEmpty(PathToRootFolder))
            {
                type = AssemblyLoader.GetTypeFromLoadedAssembly(typeFullName, PathToRootFolder);
            }

            return type;
        }

        /// <summary>
        /// Gets type using type partial name
        /// </summary>
        /// <param name="typeFullName">Type full name (required)</param>
        /// <returns>Type name</returns>
        public static string GetTypeName(string typeFullName)
        {
            // Validate arguments
            if (string.IsNullOrEmpty(typeFullName))
            {
                throw new ArgumentNullException("typeFullName");
            }

            if (typeFullName.Contains(","))
            {
                return typeFullName.Substring(0, typeFullName.IndexOf(","));
            }

            return typeFullName;
        }

        /// <summary>
        /// Gets type using type partial name
        /// </summary>
        /// <param name="typePartialName">Type partial name (required)</param>
        /// <returns>Type if found otherwise null</returns>
        public static Type? GetTypeUsingPartialName(string typePartialName)
        {
            // Validate arguments
            if (string.IsNullOrEmpty(typePartialName))
            {
                throw new ArgumentNullException("typePartialName");
            }

            string[] valueParts = typePartialName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < valueParts.Length; i++)
            {
                Type? type = null;
                try
                {
                    type = Type.GetType(typePartialName + ", " + valueParts.Concatenate('.', 0, i));
                }
                catch
                {
                }

                if (type != null)
                {
                    return type;
                }
            }

            return null;
        }
        #endregion
    }
}
