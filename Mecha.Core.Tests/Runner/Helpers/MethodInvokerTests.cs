using Mecha.Core.Runner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Mecha.Core.Tests.Runner.Helpers
{
    public class MethodInvokerTests
    {
        [Fact]
        public void Constructor_WithValidMethod_SetsMethodProperty()
        {
            // Arrange
            MethodInfo method = typeof(TestClass).GetMethod(nameof(TestClass.Add))!;

            // Act
            var invoker = new MethodInvoker<TestClass>(method);

            // Assert
            Assert.NotNull(invoker.Method);
            Assert.Equal(method, invoker.Method);
        }

        [Fact]
        public void Invoke_InstanceMethodWithReturnValue_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo method = typeof(TestClass).GetMethod(nameof(TestClass.Add))!;
            var invoker = new MethodInvoker<TestClass>(method);
            var target = new TestClass();

            // Act
            var result = invoker.Invoke(target, [3, 5]);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void Invoke_StaticMethodWithNullTarget_WorksCorrectly()
        {
            // Arrange
            MethodInfo method = typeof(TestClassStatic).GetMethod(nameof(TestClassStatic.CreateInstance))!;
            var invoker = new MethodInvoker<TestClass>(method);
            Type TargetType = null;
            object[] args = [];
            // Act
            var result = Assert.Throws<ArgumentNullException>(() => invoker.Invoke(null, [TargetType, args]));
            // Assert
            Assert.Equal("type", result.ParamName);
        }

        [Fact]
        public void Invoke_StaticMethodWithReturnValue_ReturnsCorrectResult()
        {
            // Arrange
            MethodInfo method = typeof(TestClass).GetMethod(nameof(TestClass.StaticAdd))!;
            var invoker = new MethodInvoker<TestClass>(method);

            // Act
            var result = invoker.Invoke(null, [10, 20]);

            // Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void Invoke_VoidMethod_ReturnsNull()
        {
            // Arrange
            MethodInfo method = typeof(TestClass).GetMethod(nameof(TestClass.SetValue))!;
            var invoker = new MethodInvoker<TestClass>(method);
            var target = new TestClass();

            // Act
            var result = invoker.Invoke(target, [42]);

            // Assert
            Assert.Null(result);
            Assert.Equal(42, target.Value);
        }

        [Fact]
        public void Invoke_WithEmptyParameters_WorksForParameterlessMethods()
        {
            // Arrange
            MethodInfo method = typeof(object).GetMethod(nameof(object.GetHashCode))!;
            var invoker = new MethodInvoker<object>(method);
            var target = new object();

            // Act
            var result = invoker.Invoke(target, []);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<int>(result);
        }

        [Fact]
        public void Invoke_WithNullTarget_WorksForStaticMethods()
        {
            // Arrange
            MethodInfo method = typeof(TestClass).GetMethod(nameof(TestClass.StaticAdd))!;
            var invoker = new MethodInvoker<TestClass>(method);

            // Act
            var result = invoker.Invoke(null, [1, 2]);

            // Assert
            Assert.Equal(3, result);
        }

        private static class TestClassStatic
        {
            public static object CreateInstance(Type type, params object[] args) => type is null ? throw new ArgumentNullException(nameof(type)) : Activator.CreateInstance(type, args);
        }

        private class TestClass
        {
            public int Value { get; set; }

            public static int StaticAdd(int a, int b) => a + b;

            public static void StaticSetValue(ref int value) => value = 42;

            public int Add(int a, int b) => a + b;

            public void OutMethod(out int result) => result = 100;

            public int RefMethod(ref int value)
            {
                value *= 2;
                return value;
            }

            public void SetValue(int value) => Value = value;
        }
    }
}