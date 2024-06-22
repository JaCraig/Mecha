using Canister.Interfaces;
using Mecha.Core.ExtensionMethods;
using Mecha.Core.Tests.BaseClasses;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using Xunit;

namespace Mecha.Core.Tests.ExtensionMethods
{
    public class CanisterMethodsTests : TestBaseClass
    {
        protected override Type ObjectType => typeof(CanisterExtensions);

        [Fact]
        public static void CanCallRegisterMecha()
        {
            // Arrange
            ICanisterConfiguration Bootstrapper = Substitute.For<ICanisterConfiguration>();

            // Act
            ICanisterConfiguration? Result = Bootstrapper.RegisterMecha();

            // Assert
            Assert.NotNull(Result);
        }
    }
}