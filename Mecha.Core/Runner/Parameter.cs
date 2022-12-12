using Mecha.Core.ExtensionMethods;
using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Mutator;
using Mecha.Core.Shrinker;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Mecha.Core.Runner
{
    /// <summary>
    /// Parameter value
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Runner.Parameter"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public Parameter(ParameterInfo parameter)
        {
            ParameterInfo = parameter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Runner.Parameter"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="value">The value.</param>
        public Parameter(ParameterInfo parameter, object? value)
        {
            ParameterInfo = parameter;
            if (value is not null && !parameter.ParameterType.IsAssignableFrom(value?.GetType()))
                value = null;
            if (value is null)
                DefaultValueLookup.Values.TryGetValue(parameter.ParameterType.GetHashCode(), out value);
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="value">The value.</param>
        /// <param name="shrinkCount">The shrink count.</param>
        /// <param name="mutationCount">The mutation count.</param>
        public Parameter(ParameterInfo parameter, object? value, int shrinkCount, int mutationCount) : this(parameter, value)
        {
            ShrinkCount = shrinkCount;
            MutationCount = mutationCount;
        }

        /// <summary>
        /// Gets the mutate count.
        /// </summary>
        /// <value>The mutate count.</value>
        public int MutationCount { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public ParameterInfo ParameterInfo { get; }

        /// <summary>
        /// Gets or sets the shrink count.
        /// </summary>
        /// <value>The shrink count.</value>
        public int ShrinkCount { get; private set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public object? Value { get; private set; }

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns></returns>
        public Parameter Copy()
        {
            return new Parameter(ParameterInfo, Value, ShrinkCount, MutationCount);
        }

        /// <summary>
        /// Mutates the parameter.
        /// </summary>
        /// <param name="mutator">The mutator.</param>
        /// <param name="results">The results.</param>
        /// <returns>True if it is mutated, false otherwise.</returns>
        public bool Mutate(MutatorManager? mutator, List<RunResult> results)
        {
            if (mutator is null)
                return false;
            if (results.Any(x => x.Parameters.Any(y => Same(y, this))))
                return false;

            var FinalValue = mutator.Mutate(Value);
            if (Same(FinalValue, Value))
                return false;
            Value = FinalValue;
            ++MutationCount;
            return true;
        }

        /// <summary>
        /// Determines if the values are the same
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if they are, false otherwise</returns>
        public bool Same(Parameter value)
        {
            return Same(this, value);
        }

        /// <summary>
        /// Shrinks the specified shrinker.
        /// </summary>
        /// <param name="shrinker">The shrinker.</param>
        /// <param name="results">The results.</param>
        public bool Shrink(ShrinkerManager? shrinker, List<RunResult> results)
        {
            if (shrinker is null)
                return false;
            if (results.Any(x => x.Parameters.Any(y => Same(y, this))))
                return false;

            var FinalValue = shrinker.Shrink(Value);
            if (Same(FinalValue, Value))
                return false;
            Value = FinalValue;
            ++ShrinkCount;
            return true;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{ParameterInfo?.Name} shrink count: {ShrinkCount}";
        }

        /// <summary>
        /// Sames the specified value1.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>True if they are the same, false otherwise.</returns>
        private static bool Same(Parameter? value1, Parameter? value2)
        {
            if (value1 is null || value2 is null || value1.ParameterInfo != value2.ParameterInfo)
                return false;
            return Same(value1.Value, value2.Value);
        }

        /// <summary>
        /// Sames the specified value1.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns></returns>
        private static bool Same(object? value1, object? value2)
        {
            if (value1 is null && value2 is null)
                return true;
            if (value1 is null || value2 is null)
                return false;
            if (value1.IsInfinite() && value2.IsInfinite())
                return true;
            if (value1.IsInfinite() || value2.IsInfinite())
                return false;
            try
            {
                return JsonSerializer.Serialize(value1) == JsonSerializer.Serialize(value2);
            }
            catch { return false; }
        }
    }
}