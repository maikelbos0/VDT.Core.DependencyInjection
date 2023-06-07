﻿using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Options for a method that returns service types for a given implementation type
    /// </summary>
    [Obsolete]
    public class ServiceTypeProviderOptions {
        /// <summary>
        /// Create options for a method that returns service types for a given implementation type
        /// </summary>
        /// <param name="serviceTypeProvider">Method that returns service types for a given implementation type</param>
        /// <remarks>When using decorators, the service types must differ from the implementation type</remarks>
        public ServiceTypeProviderOptions(ServiceTypeProvider serviceTypeProvider) {
            ServiceTypeProvider = serviceTypeProvider;
        }

        /// <summary>
        /// Method that returns service types for a given implementation type
        /// </summary>
        /// <remarks>When using decorators, the service types must differ from the implementation type</remarks>
        public ServiceTypeProvider ServiceTypeProvider { get; set; }

        /// <summary>
        /// Method that returns a service lifetime for a given service and implementation type to be registered
        /// </summary>
        public ServiceLifetimeProvider? ServiceLifetimeProvider { get; set; }
    }
}
