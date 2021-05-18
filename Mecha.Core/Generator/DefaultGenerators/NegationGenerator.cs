using Mecha.Core.Generator.DefaultGenerators.Utils;
using Mecha.Core.Generator.Interfaces;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Negation generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class NegationGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NegationGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public NegationGenerator(Mirage.Random random)
        {
            RandomObj = random;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => 1;

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
        public bool CanGenerate(ParameterInfo? parameter)
        {
            if (parameter is null)
                return false;
            return !parameter.HasDefaultValue
                && (NegateValueLookup.Negate?.ContainsKey(parameter.ParameterType.GetHashCode()) ?? false);
        }

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The next object.</returns>
        public object? Next(ParameterInfo? parameter, object? min, object? max)
        {
            if (parameter is null || !CanGenerate(parameter))
                return null;
            var Value = RandomObj.Next(0, 1) == 0 ? min : max;
            if (Value is null)
                return null;
            try
            {
                return NegateValueLookup.Negate?[parameter.ParameterType.GetHashCode()](Value) ?? false;
            }
            catch
            {
                return Value;
            }
        }
    }
}