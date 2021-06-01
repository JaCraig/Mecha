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

namespace Mecha.Core.Modules
{
    /// <summary>
    /// Canister module
    /// </summary>
    /// <seealso cref="IModule"/>
    public class CanisterModule : IModule
    {
        /// <summary>
        /// Order to run this in
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public void Load(IBootstrapper? bootstrapper)
        {
            bootstrapper?.RegisterAll<ISerializer>(ServiceLifetime.Singleton)
                         .RegisterAll<IDatasource>(ServiceLifetime.Singleton)
                         .Register<DataManager>(ServiceLifetime.Singleton)
                         .RegisterAll<IGenerator>(ServiceLifetime.Singleton)
                         .Register<GeneratorManager>(ServiceLifetime.Singleton)
                         .Register<Mech>(ServiceLifetime.Singleton)
                         .Register<TestRunnerManager>(ServiceLifetime.Singleton)
                         .RegisterAll<IRunner>(ServiceLifetime.Singleton)
                         .RegisterAll<IShrinker>(ServiceLifetime.Singleton)
                         .Register<ShrinkerManager>(ServiceLifetime.Singleton)
                         .RegisterAll<IMutator>(ServiceLifetime.Singleton)
                         .Register<MutatorManager>(ServiceLifetime.Singleton);
        }
    }
}