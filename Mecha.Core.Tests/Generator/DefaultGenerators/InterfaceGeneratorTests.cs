using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System;
using System.Reflection;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    /// <summary>
    /// Interface generator test
    /// </summary>
    /// <seealso cref="Mecha.Core.Tests.BaseClasses.TestBaseClass{Mecha.Core.Generator.DefaultGenerators.InterfaceGenerator}"/>
    public class InterfaceGeneratorTest : TestBaseClass<InterfaceGenerator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceGeneratorTest"/> class.
        /// </summary>
        public InterfaceGeneratorTest()
        {
            TestObject = new InterfaceGenerator();
            MethodParam = Array.Find(typeof(InterfaceGeneratorTest).GetMethods(BindingFlags.NonPublic | BindingFlags.Static), x => string.Equals(x.Name, "InterfaceMethod", StringComparison.OrdinalIgnoreCase))?.GetParameters()[0];
        }

        /// <summary>
        /// Gets the method parameter.
        /// </summary>
        /// <value>The method parameter.</value>
        private ParameterInfo? MethodParam { get; }

        /// <summary>
        /// Ranges the test.
        /// </summary>
        [Property]
        public void RangeTest() => Assert.IsAssignableFrom<IConvertible>(TestObject.Next(MethodParam, null, null));

        /// <summary>
        /// Interfaces the method.
        /// </summary>
        /// <param name="convertible">The convertible.</param>
        private static void InterfaceMethod(IConvertible convertible)
        {
        }
    }
}