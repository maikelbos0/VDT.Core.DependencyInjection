﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection.Attributes {
    /// <summary>
    /// Marks a service to be registered as scoped with the given implementation type when calling <see cref="DependencyInjection.ServiceCollectionExtensions.AddServices(IServiceCollection, Action{ServiceRegistrationOptions})"/>
    /// with <see cref="ServiceRegistrationOptionsExtensions.AddAttributeServiceRegistrationProviders(ServiceRegistrationOptions)"/> called on the <see cref="ServiceRegistrationOptions"/> builder action
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ScopedServiceAttribute : Attribute, IServiceAttribute {
        /// <summary>
        /// The type to use as implementation for this service
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// The lifetime of services marked with this attribute is <see cref="ServiceLifetime.Scoped"/>
        /// </summary>
        public ServiceLifetime ServiceLifetime => ServiceLifetime.Scoped;

        /// <summary>
        /// Marks a service to be registered as scoped with the given implementation type when calling <see cref="DependencyInjection.ServiceCollectionExtensions.AddServices(IServiceCollection, Action{ServiceRegistrationOptions})"/>
        /// with <see cref="ServiceRegistrationOptionsExtensions.AddAttributeServiceRegistrationProviders(ServiceRegistrationOptions)"/> called on the <see cref="ServiceRegistrationOptions"/> builder action
        /// </summary>
        /// <param name="implementationType">The type to use as implementation for this service</param>
        /// <remarks>When using decorators, the type specified in <paramref name="implementationType"/> must differ from the service type</remarks>
        public ScopedServiceAttribute(Type implementationType) {
            ImplementationType = implementationType;
        }
    }
}
