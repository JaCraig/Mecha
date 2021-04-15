using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests
{
    public class UnitTest1
    {
        [Property(GenerationCount = 10, MaxDuration = 1000)]
        public void Test1(int data)
        {
        }

        [Property(GenerationCount = 10, MaxDuration = 1000)]
        public void Test2(int data)
        {
        }

        [Property(GenerationCount = 3, MaxDuration = 1000)]
        public void Test3(bool value)
        {
            Assert.False(value);
        }
    }
}