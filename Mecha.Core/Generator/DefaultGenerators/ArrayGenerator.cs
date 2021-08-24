using Fast.Activator;
using Mecha.Core.Generator.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace Mecha.Core.Generator.DefaultGenerators
{
    /// <summary>
    /// Array generator
    /// </summary>
    /// <seealso cref="IGenerator"/>
    public class ArrayGenerator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayGenerator"/> class.
        /// </summary>
        /// <param name="random">The random.</param>
        public ArrayGenerator(Mirage.Random random)
        {
            Random = random;
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        private Mirage.Random Random { get; }

        /// <summary>
        /// Determines whether this instance can generate the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// <c>true</c> if this instance can generate the specified parameter; otherwise, <c>false</c>.
        /// </returns>
        public bool CanGenerate(ParameterInfo? parameter)
        {
            return parameter?.HasDefaultValue == false && parameter.ParameterType.IsArray;
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
            var Amount = Random.Next(0, 100);
            var ElementType = parameter.ParameterType.GetElementType();
            var ArrayInstance = (Array)FastActivator.CreateInstance(parameter.ParameterType, new object[] { Amount });
            if (!ElementType.GetConstructors().Any(IsDefaultConstructor))
                return ArrayInstance;
            int Index = 0;
            foreach (var Item in Random.Next(ElementType, Amount))
            {
                ArrayInstance.SetValue(Item, Index);
                ++Index;
            }
            return ArrayInstance;
        }

        /// <summary>
        /// Determines whether [is default constructor] [the specified constructor].
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        /// <returns>
        /// <c>true</c> if [is default constructor] [the specified constructor]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsDefaultConstructor(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Length == 0;
        }
    }
}