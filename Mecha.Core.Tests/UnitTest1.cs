using Mecha.xUnit;
using Xunit;

namespace Mecha.Core.Tests
{
    public class UnitTest1
    {
        [Theory]
        [FuzzData(10)]
        public void Test1(int data)
        {
        }

        [Theory]
        [FuzzData(10)]
        public void Test2(int data)
        {
        }

        [Property]
        public void Test3()
        {
        }
    }
}