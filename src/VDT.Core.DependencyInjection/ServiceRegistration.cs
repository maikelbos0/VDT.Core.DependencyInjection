﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Information for registering an implementation type
    /// </summary>
    public class ServiceRegistration {
        /// <summary>
        /// Create information for registering an implementation type
        /// </summary>
        /// <param name="serviceType">The type of the service to register the given implementation type as</param>
        public ServiceRegistration(Type serviceType) : this(serviceType, null) { }

        /// <summary>
        /// Create information for registering an implementation type
        /// </summary>
        /// <param name="serviceType">The type of the service to register the given implementation type as</param>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the service to register</param>
        public ServiceRegistration(Type serviceType, ServiceLifetime? serviceLifetime) {
            ServiceType = serviceType;
            ServiceLifetime = serviceLifetime;
        }

        /// <summary>
        /// The type of the service to register the given implementation type as
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// The <see cref="ServiceLifetime"/> for the service to register
        /// </summary>
        public ServiceLifetime? ServiceLifetime { get; set; }
    }
}
