using Mecha.Core.Datasources.Interfaces;
using Mecha.Core.ExtensionMethods;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mecha.Core.Datasources
{
    /// <summary>
    /// Data manager
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataManager"/> class.
        /// </summary>
        /// <param name="datasources">The datasources.</param>
        /// <param name="serializers">The serializers.</param>
        public DataManager(IEnumerable<IDatasource> datasources, IEnumerable<ISerializer> serializers)
        {
            var MechaCoreAssembly = TypeCache<Mech>.Assembly;
            Datasource = datasources.FirstOrDefault(x => x.GetType().Assembly != MechaCoreAssembly) ?? new DefaultDatasource();
            Serializer = serializers.FirstOrDefault(x => x.GetType().Assembly != MechaCoreAssembly) ?? new DefaultSerializer();
        }

        /// <summary>
        /// Gets the datasource.
        /// </summary>
        /// <value>The datasource.</value>
        private IDatasource Datasource { get; }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <value>The serializer.</value>
        private ISerializer Serializer { get; }

        /// <summary>
        /// Clears the specified method data.
        /// </summary>
        /// <param name="method">The method.</param>
        public void Clear(MethodInfo method)
        {
            Datasource.Clear(method);
        }

        /// <summary>
        /// Reads the specified method's data.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The data specified.</returns>
        public List<object?[]> Read(MethodInfo method)
        {
            return Datasource.Read(method, Serializer);
        }

        /// <summary>
        /// Saves the specified method's data.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="data">The data.</param>
        public void Save(MethodInfo method, object?[] data)
        {
            Datasource.Save(method, data, Serializer);
        }
    }
}