using System;
using Xunit;
using Xunit.Sdk;

namespace Mecha.xUnit
{
    /// <summary>
    /// Property attribute
    /// </summary>
    /// <seealso cref="Xunit.FactAttribute"/>
    [XunitTestCaseDiscoverer("Mecha.xUnit.PropertyDiscoverer", "Mecha.xUnit")]
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyAttribute : FactAttribute
    {
    }
}