using System;

namespace Mecha.Core
{
    /// <summary>
    /// Property attribute
    /// </summary>
    /// <seealso cref="Xunit.FactAttribute"/>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class DoNotBreakAttribute : Attribute
    {
    }
}