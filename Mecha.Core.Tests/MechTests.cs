using BigBook.ExtensionMethods;
using Mecha.Core.Tests.BaseClasses;
using Mecha.xUnit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests
{
    public static class StaticTestExtensions
    {
        public static bool TryParseDate(this string value, out DateTime date)
        {
            if (string.IsNullOrEmpty(value))
            {
                date = DateTime.MinValue;
                return false;
            }

            if (DateTime.TryParse(value, out date))
                return true;

            if (DateTime.TryParse(value.ToString("##/##/####"), out date))
                return true;

            if (!double.TryParse(value, out _))
            {
                date = DateTime.MinValue;
                return false;
            }

            date = DateTime.MaxValue;
            return true;
        }
    }

    /// <summary>
    /// Mech tests
    /// </summary>
    /// <seealso cref="TestBaseClass{Core.Shrinker.Mech}"/>
    public class MechTests : TestBaseClass<Mech>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MechTests"/> class.
        /// </summary>
        public MechTests()
        {
            TestObject = Mech.Default;
        }

        [Fact]
        public async Task BreakAsyncStaticTestExtensions()
        {
            // Arrange
            Type TestType = typeof(StaticTestExtensions);

            // Act
            await Mech.BreakAsync(TestType, Options.Default);
        }

        [Fact]
        public Task BreakStaticMethods()
        {
            System.Reflection.MethodInfo[] StaticMethods = typeof(Mech).GetMethods();
            var Tasks = new List<Task>();
            foreach (System.Reflection.MethodInfo? Method in StaticMethods.Where(x => x.IsStatic))
            {
                Tasks.Add(Mech.BreakAsync(Method, null, Options.Default));
            }
            return Task.WhenAll(Tasks);
        }

        /// <summary>
        /// Shrinks the specified value.
        /// </summary>
        [Fact]
        public async Task RunAsync()
        {
            Core.Runner.Result Result = await TestObject.RunAsync(TestMethodInfo, this, Options.Default);
            Assert.Null(Result.Exception);
            Assert.True(Result.ExecutionTime >= 0);
            Assert.NotEmpty(Result.Output ?? "");
            Assert.True(Result.Passed);
        }

        /// <summary>
        /// Validation test.
        /// </summary>
        /// <param name="value">The value.</param>
        [Property(GenerationCount = 100)]
        public void ValidationTest([Required] string value) => Assert.False(string.IsNullOrEmpty(value));
    }
}