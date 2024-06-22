using Mecha.Core.Generator.DefaultGenerators.Utils;
using Mecha.Core.Tests.BaseClasses;
using System;
using System.Collections.Generic;
using Xunit;

namespace Mecha.Core.Tests.Generator.DefaultGenerators.Utils
{
    public class NegateValueLookupTests : TestBaseClass
    {
        protected override Type ObjectType => typeof(NegateValueLookup);

        [Fact]
        public static void CanGetNegate() =>
            // Assert
            Assert.IsType<Dictionary<int, Func<object, object>>>(NegateValueLookup.Negate);

        [Fact]
        public static void CanNegate()
        {
            var Rand = new Mirage.Random();
            Dictionary<int, Func<object, object>>? Negate = NegateValueLookup.Negate;

            foreach (Type Type in new Type[]
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(int),
                typeof(ushort),
                typeof(uint),
                typeof(double),
                typeof(float),
                typeof(decimal),
                typeof(bool),
                typeof(char),
                typeof(byte?),
                typeof(sbyte?),
                typeof(short?),
                typeof(int?),
                typeof(ushort?),
                typeof(uint?),
                typeof(double?),
                typeof(float?),
                typeof(decimal?),
                typeof(bool?),
                typeof(char?)
            })
            {
                // Act
                var Value1 = Rand.Next(Type);
                var Value2 = Negate[Type.GetHashCode()](Value1!);

                // Assert
                Assert.NotEqual(Value1, Value2);
            }
        }
    }
}