using Mecha.xUnit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests
{
    public static class ExtensionTestClass
    {
        public static bool ExtensionMethod1(this TestClass value)
        {
            return value.Method3(null);
        }
    }

    public class TestClass
    {
        public bool FileStreamTest(System.IO.FileStream stream)
        {
            stream?.Read(new byte[100], 0, 100);
            return true;
        }

        public void Method1()
        {
        }

        public void Method2(string value)
        {
        }

        [DoNotBreak]
        public bool Method3(string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));
            return false;
        }

        public bool StreamTest(System.IO.Stream stream)
        {
            stream?.Read(new byte[100], 0, 100);
            return true;
        }
    }

    public class UnitTest1
    {
        [Fact]
        public Task BreakTest()
        {
            return Mech.BreakAsync(new TestClass());
        }

        [Fact]
        public Task BreakTest2()
        {
            return Mech.BreakAsync(() => new TestClass().ExtensionMethod1());
        }

        [Fact]
        public Task BreakTest3()
        {
            return Mech.BreakAsync((TestClass test) => test?.ExtensionMethod1());
        }

        [Fact]
        public Task BreakTest4()
        {
            return Mech.BreakAsync<TestClass>();
        }

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

        [Property(GenerationCount = 10, MaxDuration = 1000)]
        public void Test5(string value, Dictionary<int, string> dictionary)
        {
            Assert.True(dictionary.ContainsKey(1));
        }

        [Property(GenerationCount = 10, MaxDuration = 1000)]
        public async Task Test6(HttpClient httpClient)
        {
            if (httpClient is null)
                return;
            try
            {
                var Result = await httpClient.GetAsync("https://www.google.com").ConfigureAwait(false);
                Assert.True(Result.IsSuccessStatusCode);
            }
            catch { }
        }
    }
}