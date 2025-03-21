﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mecha.Core.ExtensionMethods
{
    /// <summary>
    /// Services
    /// </summary>
    internal static class Services
    {
        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <value>The service provider.</value>
        public static IServiceProvider? ServiceProvider
        {
            get
            {
                if (_ServiceProvider is not null)
                    return _ServiceProvider;
                lock (_ServiceProviderLock)
                {
                    if (_ServiceProvider is not null)
                        return _ServiceProvider;
                    _ServiceProvider = new ServiceCollection().AddCanisterModules()?.BuildServiceProvider();
                }
                return _ServiceProvider;
            }
        }

        /// <summary>
        /// The service provider lock
        /// </summary>
        private static readonly object _ServiceProviderLock = new();

        /// <summary>
        /// The service provider
        /// </summary>
        private static IServiceProvider? _ServiceProvider;
    }
}