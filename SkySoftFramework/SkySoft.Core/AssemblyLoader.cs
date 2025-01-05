using System.Reflection;
using System.Runtime.Loader;

namespace SkySoft.Core
{
    /// <summary>
    /// Provides assembly loader functionality
    /// </summary>
    public class AssemblyLoader
    {
        #region Properties
        /// <summary>
        /// Gets or sets list of loaded assemblies
        /// </summary>
        public static Dictionary<string, Assembly> LoadedAssemblies
        {
            get; set;
        } = new Dictionary<string, Assembly>();
        #endregion

        /// <summary>
        /// Gets type from loaded assembly
        /// </summary>
        /// <param name="typeName">Type name</param>
        /// <param name="pathToRootFolder">Path to root folder</param>
        /// <returns>Type from loaded assembly</returns>
        public static Type? GetTypeFromLoadedAssembly(string typeName, string pathToRootFolder)
        {
            string assemblyTypeName = typeName.Substring(typeName.IndexOf(",") + 1).Trim();
            Assembly? assembly = null;
            if (LoadedAssemblies.ContainsKey(assemblyTypeName))
            {
                assembly = LoadedAssemblies[assemblyTypeName];
            }
            else
            {
                assembly = AssemblyLoader.Load(Path.Combine(pathToRootFolder, assemblyTypeName + ".dll"));
            }

            if (assembly == null)
            {
                return null;
            }

            typeName = typeName.Substring(0, typeName.IndexOf(",")).Trim();
            return assembly.ExportedTypes.FirstOrDefault(type => type.FullName == typeName);
        }

        /// <summary>
        /// Loads assembly
        /// </summary>
        /// <param name="fullPathToAssemblyFile">Full path to assembly file</param>
        /// <returns>Loaded assembly</returns>
        public static Assembly? Load(string fullPathToAssemblyFile)
        {
            if (string.IsNullOrEmpty(fullPathToAssemblyFile))
            {
                throw new ArgumentNullException(nameof(fullPathToAssemblyFile));
            }

            string fileName = Path.GetFileName(fullPathToAssemblyFile);
            string? directoryName = Path.GetDirectoryName(fullPathToAssemblyFile);
            Assembly? assembly = null;
            if (Path.Exists(fullPathToAssemblyFile) && !string.IsNullOrEmpty(directoryName))
            {
                assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(fullPathToAssemblyFile);
                if (assembly != null)
                {
                    LoadedAssemblies.Add(Path.GetFileNameWithoutExtension(fullPathToAssemblyFile), assembly);
                    LoadReferencedAssemblies(assembly, fileName, directoryName);
                }
            }

            return assembly;
        }

        /// <summary>
        /// Loads referenced assemblies
        /// </summary>
        /// <param name="assembly">Assembly instance</param>
        /// <param name="assemblyFileName">Assembly file name</param>
        /// <param name="directoryName">Directory name</param>
        private static void LoadReferencedAssemblies(Assembly assembly, string assemblyFileName, string directoryName)
        {
            List<string> filesInDirectory = Directory.GetFiles(directoryName).Where(x => x != assemblyFileName).Select(x => Path.GetFileNameWithoutExtension(x)).ToList();
            AssemblyName[] namesOfReferencedAssemblies = assembly.GetReferencedAssemblies();
            foreach (var nameOfReferencedAssemblies in namesOfReferencedAssemblies)
            {
                if (!string.IsNullOrEmpty(nameOfReferencedAssemblies.Name) &&!LoadedAssemblies.ContainsKey(nameOfReferencedAssemblies.Name))
                {
                    if (filesInDirectory.Contains(nameOfReferencedAssemblies.Name))
                    {
                        assemblyFileName = nameOfReferencedAssemblies.Name + ".dll";
                        string path = Path.Combine(directoryName, assemblyFileName);
                        Assembly loadedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                        if (loadedAssembly != null)
                        {
                            LoadedAssemblies.Add(nameOfReferencedAssemblies.Name, loadedAssembly);
                            LoadReferencedAssemblies(loadedAssembly, assemblyFileName, directoryName);
                        }
                    }
                }
            }
        }
    }
}
