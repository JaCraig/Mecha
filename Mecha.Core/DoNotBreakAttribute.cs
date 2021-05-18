using System;

namespace Mecha.Core
{
    /// <summary>
    /// Property attribute
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
    public class DoNotBreakAttribute : Attribute
    {
    }
}