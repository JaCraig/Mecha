using Mecha.Core.Generator.DefaultGenerators.Utils;
using Mecha.Core.Generator.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Min Boundary generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class MinBoundaryGenerator : IGenerator
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => 0;

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo parameter)
        {
            if (parameter is null)
                return false;
            return !parameter.HasDefaultValue
                && (MinValueLookup.Min?.ContainsKey(parameter.ParameterType.GetHashCode()) ?? false);
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The next object.</returns>
        public object Next(ParameterInfo parameter, object? min, object? max)
        {
            if (parameter is null)
                return null;
            var Key = parameter.ParameterType.GetHashCode();
            var Range = parameter.GetCustomAttribute<RangeAttribute>();
            return Range?.Minimum ?? MinValueLookup.Min?[Key] ?? false;
        }
    }
}