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

using Mecha.Core.Datasources.Interfaces;
using System;
using System.Text.Json;

namespace Mecha.Core.Datasources
{
    /// <summary>
    /// Default serializer (goes to JSON)
    /// </summary>
    /// <seealso cref="ISerializer"/>
    public class DefaultSerializer : ISerializer
    {
        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="data">The data.</param>
        /// <returns>The deserialized value.</returns>
        public object? Deserialize(Type objectType, string? data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return null;
            return JsonSerializer.Deserialize(data, objectType);
        }

        /// <summary>
        /// Serializes the specified data.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="data">The data.</param>
        /// <returns>The serialized value used for storage.</returns>
        public string? Serialize(Type objectType, object? data)
        {
            return JsonSerializer.Serialize(data, objectType);
        }
    }
}