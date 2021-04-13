﻿using System;

namespace Mecha.xUnit
{
    /// <summary>
    /// Properties attribute
    /// </summary>
    /// <seealso cref="System.Attribute"/>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PropertiesAttribute : PropertyAttribute
    {
        /// <summary>
        /// Gets the number of randomly generated items to create.
        /// </summary>
        /// <value>The number of randomly generated items to create.</value>
        public int? GenerationCount { get; set; }

        /// <summary>
        /// Gets the max duration to run the tests for.
        /// </summary>
        /// <value>The max duration to run the tests for.</value>
        public int? MaxDuration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="PropertyAttribute"/> is verbose.
        /// </summary>
        /// <value><c>true</c> if verbose; otherwise, <c>false</c>.</value>
        public bool? Verbose { get; set; }
    }
}