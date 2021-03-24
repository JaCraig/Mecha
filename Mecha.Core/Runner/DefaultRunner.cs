using Mecha.Core.Runner.Interfaces;
using System;
using System.Reflection;

namespace Mecha.Core.Runner
{
    /// <summary>
    /// Default runner
    /// </summary>
    /// <seealso cref="IRunner"/>
    public class DefaultRunner : IRunner
    {
        public Result Run(MethodInfo runMethod, object? target)
        {
            throw new NotImplementedException();
        }
    }
}