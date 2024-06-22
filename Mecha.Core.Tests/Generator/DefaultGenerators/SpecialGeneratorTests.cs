using Mecha.Core.Generator.DefaultGenerators;
using Mecha.Core.Tests.BaseClasses;
using System;
using System.Reflection;
using Xunit;
using Random = Mirage.Random;

namespace Mecha.Core.Tests.Generator.DefaultGenerators
{
    public class SpecialGeneratorTests : TestBaseClass<SpecialGenerator>
    {
        public SpecialGeneratorTests()
        {
            _Random = new Random();
            _TestClass = new SpecialGenerator(_Random);
            TestObject = new SpecialGenerator(_Random);
        }

        private readonly Random _Random;
        private readonly SpecialGenerator _TestClass;

        [Fact]
        public void CanCallCanGenerate()
        {
            // Arrange
            MethodInfo? TestMethod = typeof(TestClass).GetMethod(nameof(TestClass.TestMethod));
            ParameterInfo Parameter = TestMethod!.GetParameters()[0];

            // Act
            var Result = _TestClass.CanGenerate(Parameter);

            // Assert
            Assert.True(Result);
        }

        [Fact]
        public void CanCallNext()
        {
            // Arrange
            MethodInfo? TestMethod = typeof(TestClass).GetMethod(nameof(TestClass.TestMethod));
            ParameterInfo Parameter = TestMethod!.GetParameters()[0];
            const char Min = char.MinValue;
            const char Max = char.MaxValue;

            // Act
            Core.Generator.ParameterValue Result = _TestClass.Next(Parameter, Min, Max);

            // Assert
            Assert.NotNull(Result);
        }

        [Fact]
        public void CanConstruct()
        {
            // Act
            var Instance = new SpecialGenerator(_Random);

            // Assert
            Assert.NotNull(Instance);
        }

        [Fact]
        public void CanGetOrder()
        {
            // Assert
            var Result = Assert.IsType<int>(_TestClass.Order);
            Assert.Equal(int.MinValue, Result);
        }

        public class TestClass
        {
            public Memory<char> TestMethod(Memory<char> value) => value;
        }
    }
}