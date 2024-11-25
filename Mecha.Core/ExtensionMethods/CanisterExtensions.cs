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

using BigBook.Registration;
using Canister.Interfaces;
using Mecha.Core.Datasources;
using Mecha.Core.Datasources.Interfaces;
using Mecha.Core.Generator;
using Mecha.Core.Generator.Interfaces;
using Mecha.Core.Mutator;
using Mecha.Core.Mutator.Interfaces;
using Mecha.Core.Runner;
using Mecha.Core.Runner.Interfaces;
using Mecha.Core.Shrinker;
using Mecha.Core.Shrinker.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Canister methods
    /// </summary>
    public static class CanisterMethods
    {
        /// <summary>
        /// Registers the system with canister.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <returns>The bootstrapper.</returns>
        public static ICanisterConfiguration? RegisterMecha(this ICanisterConfiguration? bootstrapper)
        {
            return bootstrapper?.AddAssembly(typeof(CanisterMethods).Assembly)
                                .RegisterMirage()
                                .RegisterFileCurator()
                                .RegisterBigBookOfDataTypes();
        }

        /// <summary>
        /// Registers the Mecha services with the provided IServiceCollection.
        /// </summary>
        /// <param name="services">The service collection to register the services with.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection? RegisterMecha(this IServiceCollection? services)
        {
            if (services.Exists<DataManager>())
                return services;
            return services?.AddAllSingleton<ISerializer>()
                         ?.AddAllSingleton<IDatasource>()
                         ?.AddSingleton<DataManager>()
                         ?.AddAllSingleton<IGenerator>()
                         ?.AddSingleton<GeneratorManager>()
                         ?.AddSingleton<Mech>()
                         ?.AddSingleton<TestRunnerManager>()
                         ?.AddAllSingleton<IRunner>()
                         ?.AddAllSingleton<IShrinker>()
                         ?.AddSingleton<ShrinkerManager>()
                         ?.AddAllSingleton<IMutator>()
                         ?.AddSingleton<MutatorManager>()
                         ?.RegisterMirage()
                         ?.RegisterFileCurator()
                         ?.RegisterBigBookOfDataTypes();
        }
    }
}