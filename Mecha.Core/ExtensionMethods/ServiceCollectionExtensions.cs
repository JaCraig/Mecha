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

using Mecha.Core.Datasources;
using Mecha.Core.Datasources.Defaults;
using Mecha.Core.Datasources.Interfaces;
using Mecha.Core.Generator;
using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Generator.Interfaces;
using Mecha.Core.Shrinker;
using Mecha.Core.Shrinker.Defaults;
using Mecha.Core.Shrinker.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// ServiceCollection extension methods.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds mecha to the service collection.
        /// </summary>
        /// <param name="serviceDescriptors">The service descriptors.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection? AddMecha(this IServiceCollection? serviceDescriptors)
        {
            return serviceDescriptors?.AddSingleton<ISerializer, DefaultSerializer>()
                                .AddSingleton<IDatasource, DefaultDatasource>()
                                .AddSingleton<DataManager>()
                                .AddSingleton<GeneratorManager>()
                                .AddSingleton<IGenerator, MaxBoundaryGenerator>()
                                .AddSingleton<IGenerator, MinBoundaryGenerator>()
                                .AddSingleton<IGenerator, DefaultGenerator>()
                                .AddSingleton<IGenerator, DefaultValueGenerator>()
                                .AddSingleton<IGenerator, InterfaceGenerator>()
                                .AddSingleton<IGenerator, ParameterDefaultValueGenerator>()
                                .AddSingleton<IGenerator, SliceGenerator>()
                                .AddSingleton<IGenerator, NegationGenerator>()
                                .AddSingleton<IGenerator, FileStreamGenerator>()
                                .AddSingleton<IGenerator, HttpClientGenerator>()
                                .AddSingleton<IGenerator, StreamGenerator>()
                                .AddSingleton<ShrinkerManager>()
                                .AddSingleton<IShrinker, NumberShrinker>()
                                .AddSingleton<IShrinker, StringShrinker>()
                                .AddSingleton<IShrinker, ListShrinker>()
                                .AddSingleton<IShrinker, DictionaryShrinker>();
        }
    }
}