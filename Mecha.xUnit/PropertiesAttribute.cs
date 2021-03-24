using System;

namespace Mecha.xUnit
{
    /// <summary>
    /// Properties attribute
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PropertiesAttribute : PropertyAttribute
    {
    }
}