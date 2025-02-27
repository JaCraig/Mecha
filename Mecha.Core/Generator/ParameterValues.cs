﻿using BigBook;
using Mecha.Core.ExtensionMethods;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.Json;

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
            ValidationAttributes = Parameter?.GetCustomAttributes<ValidationAttribute>() ?? [];
        }

        /// <summary>
        /// Gets the generated values.
        /// </summary>
        /// <value>The generated values.</value>
        public List<ParameterValue> GeneratedValues { get; } = [];

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        public ParameterInfo? Parameter { get; set; }

        /// <summary>
        /// Gets the validation attributes.
        /// </summary>
        /// <value>The validation attributes.</value>
        private IEnumerable<ValidationAttribute> ValidationAttributes { get; }

        /// <summary>
        /// Adds the value specified if it is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddValue(ParameterValue? value)
        {
            if (value is null)
                return;
            if (GeneratedValues.Any(x => x.Value == value.Value) || ValidationAttributes.Any(x => !x.IsValid(value.Value)))
                return;
            _ = GeneratedValues.AddIfUnique(Same, value);
        }

        /// <summary>
        /// Determines if the 2 arrays are the same.
        /// </summary>
        /// <param name="paramValue1">The parameter value1.</param>
        /// <param name="paramValue2">The parameter value2.</param>
        /// <returns>True if they are, false otherwise.</returns>
        private bool Same(ParameterValue paramValue1, ParameterValue paramValue2)
        {
            var Value1 = paramValue1.Value;
            var Value2 = paramValue2.Value;
            if (Value1.IsInfinite() && Value2.IsInfinite())
                return true;
            if (Value1.IsInfinite() || Value2.IsInfinite())
                return false;
            try
            {
                return (Value1 is null && Value2 is null)
                    || (Value1 is not null
                        && Value2 is not null
                        && JsonSerializer.Serialize(Value1) == JsonSerializer.Serialize(Value2));
            }
            catch { return false; }
        }
    }
}