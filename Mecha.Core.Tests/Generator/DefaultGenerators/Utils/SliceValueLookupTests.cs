using Mecha.Core.Generator.DefaultGenerators.Utils;
using Mecha.Core.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators.Utils
{
    public class SliceValueLookupTests : TestBaseClass
    {
        protected override Type ObjectType => typeof(SliceValueLookup);

        [Fact]
        public static void CanGetSlice() =>
            // Assert
            Assert.IsType<Dictionary<int, Func<object, object, object>>>(SliceValueLookup.Slice);

        [Fact]
        public static void TestTypes()
        {
            // Arrange
            var Rand = new Mirage.Random();
            Dictionary<int, Func<object, object, object>>? Slice = SliceValueLookup.Slice;
            foreach (Type Type in new Type[]
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(int),
                typeof(long),
                typeof(ushort),
                typeof(uint),
                typeof(ulong),
                typeof(double),
                typeof(float),
                typeof(decimal),
                typeof(char),
                typeof(byte?),
                typeof(sbyte?),
                typeof(short?),
                typeof(int?),
                typeof(long?),
                typeof(ushort?),
                typeof(uint?),
                typeof(ulong?),
                typeof(double?),
                typeof(float?),
                typeof(decimal?),
                typeof(char?),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(DateTime?),
                typeof(DateTimeOffset?),
                typeof(TimeSpan?)
            })
            {
                // Act
                var Value1 = Rand.Next(Type);
                var Value2 = Rand.Next(Type);
                var Result = Slice[Type.GetHashCode()](Value1!, Value2!);

                // Assert
                Assert.NotNull(Result);
            }
        }
    }
}