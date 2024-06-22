using Mecha.Core.ExtensionMethods;
using Mecha.Core.Tests.BaseClasses;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using Xunit;

namespace Mecha.Core.Tests.ExtensionMethods
{
    public class ServiceCollectionExtensionsTests : TestBaseClass
    {
        protected override Type ObjectType => typeof(Core.ExtensionMethods.ServiceCollectionExtensions);

        [Fact]
        public static void CanCallAddMecha()
        {
            // Arrange
            IServiceCollection ServiceDescriptors = Substitute.For<IServiceCollection>();

            // Act
            IServiceCollection? Result = ServiceDescriptors.AddMecha();

            // Assert
            Assert.NotNull(Result);
            Assert.Same(ServiceDescriptors, Result);
        }
    }
}