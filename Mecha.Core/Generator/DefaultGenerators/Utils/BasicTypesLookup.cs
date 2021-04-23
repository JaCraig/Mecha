using System;

namespace Mecha.Core.Generator.DefaultGenerators.Utils
{
    public static class BasicTypesLookup
    {
        public static Type[] Types = new Type[]
        {
            typeof(DummyClass),
            typeof(long),
            typeof(ulong),
            typeof(double),
            typeof(decimal),
            typeof(string),
            typeof(Guid),
            typeof(DateTime),
            typeof(DateTimeOffset),
            typeof(TimeSpan)
        };

        public class DummyClass
        {
        }
    }
}