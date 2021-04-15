using System.Collections.Generic;
using System.Reflection;

namespace Mecha.Core.Generator
{
    /// <summary>
    /// Parameter values
    /// </summary>
    public class ParameterValues
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterValues"/> class.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public ParameterValues(ParameterInfo parameter)
        {
            Parameter = parameter;
        }

        /// <summary>
        /// Gets the generated values.
        /// </summary>
        /// <value>The generated values.</value>
        public List<object?> GeneratedValues { get; } = new List<object?>();

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public ParameterInfo Parameter { get; set; }
    }
}