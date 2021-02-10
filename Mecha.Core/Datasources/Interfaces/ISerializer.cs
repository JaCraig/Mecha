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

using System;

namespace Mecha.Core.Datasources.Interfaces
{
    /// <summary>
    /// Serializer interface
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Deserializes the specified data.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="data">The data.</param>
        /// <returns>The deserialized value.</returns>
        public object? Deserialize(Type objectType, string? data);

        /// <summary>
        /// Serializes the specified data.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="data">The data.</param>
        /// <returns>The serialized value used for storage.</returns>
        public string? Serialize(Type objectType, object? data);
    }
}