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

using Mecha.Core.Generator.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Default generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class DefaultGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public DefaultGenerator(Mirage.Random random)
        {
            RandomObj = random;
            var Methods = RandomObj.GetType().GetMethods();
            GenericRandMethod = System.Array.Find(Methods, x => x.IsGenericMethod && x.GetParameters().Length == 2);
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => int.MaxValue;

        /// <summary>
        /// Gets the generic rand method.
        /// </summary>
        /// <value>The generic rand method.</value>
        private MethodInfo GenericRandMethod { get; }

        /// <summary>
        /// Gets the random object.
        /// </summary>
        /// <value>The random object.</value>
        private Mirage.Random RandomObj { get; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter)
        {
            return !parameter.HasDefaultValue && !parameter.ParameterType.IsInterface;
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The next object.</returns>
        public object? Next(ParameterInfo parameter, object min, object max)
        {
            var GenericMethod = GenericRandMethod.MakeGenericMethod(parameter.ParameterType);
            var ReturnValue = GenericMethod.Invoke(RandomObj, new object[] { min, max });
            var ValidationRules = parameter.GetCustomAttributes<ValidationAttribute>();
            if (ValidationRules?.Any() == true)
            {
                while (!ValidationRules.All(x => x.IsValid(ReturnValue)))
                {
                    ReturnValue = GenericMethod.Invoke(RandomObj, new object[] { min, max });
                }
            }
            return ReturnValue;
        }
    }
}