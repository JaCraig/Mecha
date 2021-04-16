using Mecha.xUnit;
using System.Collections.Generic;
using Xunit;

namespace Mecha.Core.Tests
{
    public class UnitTest1
    {
        [Property(GenerationCount = 10, MaxDuration = 50000)]
        public void Test1(int data, List<int> list)
        {
            Assert.Single(list);
        }

        [Property(GenerationCount = 10, MaxDuration = 1000)]
        public void Test2(int data, float value2)
        {
            Assert.True(data > 10);
        }

        [Property(GenerationCount = 3, MaxDuration = 1000)]
        public void Test3(bool value)
        {
            Assert.False(value);
        }

        [Property(GenerationCount = 10, MaxDuration = 1000)]
        public void Test4(string value)
        {
            Assert.Empty(value);
        }
    }
}