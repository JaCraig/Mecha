using Fast.Activator;
using Mecha.Core.ExtensionMethods;
using Mecha.Core.Generator.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Special generator class.
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class SpecialGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialGenerator"/> class.
        /// </summary>
        /// <param name="random">The random generator.</param>
        public SpecialGenerator(Mirage.Random? random)
        {
            Random = random;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => int.MinValue;

        /// <summary>
        /// Gets the random generator.
        /// </summary>
        private Mirage.Random? Random { get; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo? parameter) => parameter?.ParameterType.IsSpecialType(out Type? _) == true;

        /// <summary>
        /// Generates the next object of the specified parameter type.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The next object.</returns>
        public ParameterValue Next(ParameterInfo? parameter, object? min, object? max)
        {
            if (parameter is null || !CanGenerate(parameter))
                return new ParameterValue("Special Case Generator", null);
            var Amount = Random?.Next(0, 100) ?? 0;
            Type? ArrayType = parameter.ParameterType.GetUnderlyingArrayType();
            Type? ElementType = ArrayType.GetElementType();
            var ArrayInstance = (Array)FastActivator.CreateInstance(ArrayType, new object[] { Amount });
            if (ElementType?.GetConstructors().Any(IsDefaultConstructor) != true)
                return new ParameterValue("Special Case Generator", ArrayInstance);
            var Index = 0;
            foreach (var Item in Random?.Next(ElementType, Amount) ?? Array.Empty<object?>())
            {
                ArrayInstance.SetValue(Item, Index);
                ++Index;
            }
            return new ParameterValue("Special Case Generator", ArrayInstance);
        }

        /// <summary>
        /// Determines whether [is default constructor] [the specified constructor].
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        /// <returns>
        /// <c>true</c> if [is default constructor] [the specified constructor]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsDefaultConstructor(ConstructorInfo constructor) => constructor.GetParameters().Length == 0;
    }
}