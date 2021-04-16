/*
Copyright 2021 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using BigBook;
using FileCurator;
using Mecha.Core.Datasources.Interfaces;
using Mecha.Core.Generator.DefaultGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Mecha.Core.Datasources
{
    /// <summary>
    /// Default datasource (exports to local directory)
    /// </summary>
    /// <seealso cref="IDatasource"/>
    public class DefaultDatasource : IDatasource
    {
        /// <summary>
        /// Gets the data directory.
        /// </summary>
        /// <value>The data directory.</value>
        private const string DataDirectory = "./Mecha/SavedTests/";

        /// <summary>
        /// Clears the specified method's param data.
        /// </summary>
        /// <param name="method">The method.</param>
        public void Clear(MethodInfo method)
        {
            var DataDirectoryInfo = GetDirectory(DataDirectory, method);
            DataDirectoryInfo.Delete();
        }

        /// <summary>
        /// Retrieves the data for the specified method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>The list of data for the method.</returns>
        public List<object?[]> Read(MethodInfo method, ISerializer serializer)
        {
            var Parameters = method.GetParameters();
            if (Parameters.Any(x => x.ParameterType.IsInterface))
                return new List<object?[]>();

            var Results = new List<object?[]>();
            var DataDirectoryInfo = GetDirectory(DataDirectory, method);
            foreach (var Directory in DataDirectoryInfo.EnumerateDirectories())
            {
                var TempResult = new object?[Parameters.Length];
                for (int x = 0; x < Parameters.Length; ++x)
                {
                    var Data = new FileInfo($"{Directory.FullName}/{x}.json").Read();
                    TempResult[x] = serializer.Deserialize(Parameters[x].ParameterType, Data);
                    if (TempResult[x] is null && DefaultValueLookup.Values.TryGetValue(Parameters[x].ParameterType.GetHashCode(), out var DefaultValue))
                        TempResult[x] = DefaultValue;
                }
                Results.AddIfUnique(Same, TempResult);
            }
            return Results;
        }

        /// <summary>
        /// Saves the specified param data for the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="paramData">The parameter data.</param>
        /// <param name="serializer">The serializer.</param>
        public void Save(MethodInfo method, object?[] paramData, ISerializer serializer)
        {
            var Parameters = method.GetParameters();
            if (Parameters.Any(x => x.ParameterType.IsInterface))
                return;

            var DataDirectoryInfo = GetDirectory(DataDirectory, method, Guid.NewGuid());

            for (int x = 0; x < Parameters.Length; ++x)
            {
                new FileInfo($"{DataDirectoryInfo.FullName}/{x}.json").Write(serializer.Serialize(Parameters[x].ParameterType, paramData[x]) ?? "");
            }
        }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <param name="dataDirectory">The data directory.</param>
        /// <param name="method">The method.</param>
        /// <returns>The directory specified.</returns>
        private static DirectoryInfo GetDirectory(string dataDirectory, MethodInfo method)
        {
            return new DirectoryInfo(RemoveIllegalDirectoryNameCharacters($"{dataDirectory}{method.DeclaringType.Namespace}/{method.DeclaringType.GetName().Replace(method.DeclaringType.Namespace + ".", "")}/{method.Name}"));
        }

        /// <summary>
        /// Gets the directory.
        /// </summary>
        /// <param name="dataDirectory">The data directory.</param>
        /// <param name="method">The method.</param>
        /// <param name="guid">The unique identifier.</param>
        /// <returns>The directory specified.</returns>
        private static DirectoryInfo GetDirectory(string dataDirectory, MethodInfo method, Guid guid)
        {
            return new DirectoryInfo($"{GetDirectory(dataDirectory, method).FullName}/{guid}");
        }

        /// <summary>
        /// Removes illegal characters from a directory
        /// </summary>
        /// <param name="directoryName">Directory name</param>
        /// <param name="replacementChar">Replacement character</param>
        /// <returns>DirectoryName with all illegal characters replaced with ReplacementChar</returns>
        private static string RemoveIllegalDirectoryNameCharacters(string directoryName, char replacementChar = '_')
        {
            if (string.IsNullOrEmpty(directoryName))
                return directoryName;
            var InvalidChars = System.IO.Path.GetInvalidPathChars();
            for (int i = 0, maxLength = InvalidChars.Length; i < maxLength; i++)
            {
                char Char = InvalidChars[i];
                directoryName = directoryName.Replace(Char, replacementChar);
            }

            return directoryName;
        }

        /// <summary>
        /// Determines if the 2 arrays are the same.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>True if they are, false otherwise.</returns>
        private bool Same(object?[] value1, object?[] value2)
        {
            if (value1 is null || value2 is null)
                return false;
            if (value1.Length != value2.Length)
                return false;
            for (int x = 0; x < value1.Length; ++x)
            {
                var Value1 = JsonSerializer.Serialize(value1[x]);
                var Value2 = JsonSerializer.Serialize(value2[x]);
                if (Value1 != Value2)
                    return false;
            }
            return true;
        }
    }
}