using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Commonly used implementations of the <see cref="ServiceTypeProvider"/> delegate
    /// </summary>
    public static class DefaultServiceRegistrationProviders {
        /// <summary>
        /// Returns the interface for implementation types if only a single interface is found; otherwise returns no service types
        /// </summary>
        /// <param name="implementationType">The implementation type to provide service types for</param>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the service types to register</param>
        /// <returns>A single interface type if only one is available</returns>
        public static IEnumerable<ServiceRegistration> SingleInterface(Type implementationType, ServiceLifetime? serviceLifetime) {
            var serviceTypes = implementationType.GetInterfaces();

            if (serviceTypes.Length == 1) {
                return serviceTypes.Select(serviceType => new ServiceRegistration(serviceType, serviceLifetime));
            }

            return Enumerable.Empty<ServiceRegistration>();
        }

        /// <summary>
        /// Returns all interfaces that match the implementation type name with an "I" prefix; e.g. MyService and IMyService
        /// </summary>
        /// <param name="implementationType">The implementation type to provide service types for</param>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the service types to register</param>
        /// <returns>All matching interface types</returns>
        public static IEnumerable<ServiceRegistration> InterfaceByName(Type implementationType, ServiceLifetime? serviceLifetime) 
            => implementationType.GetInterfaces()
                .Where(serviceType => serviceType.Name == $"I{implementationType.Name}")
                .Select(serviceType => new ServiceRegistration(serviceType, serviceLifetime));

        /// <summary>
        /// Create a service registration provider that finds all implementations of a generic interface
        /// </summary>
        /// <param name="genericServiceType">The generic interface type definition to match implementation types to</param>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the service types to register</param>
        /// <returns>A <see cref="ServiceTypeProvider"/> that finds any matching constructed service types for an implementation type</returns>
        public static ServiceRegistrationProvider CreateGenericInterfaceRegistrationProvider(Type genericServiceType, ServiceLifetime? serviceLifetime) {
            if (!genericServiceType.IsGenericTypeDefinition) {
                throw new ServiceRegistrationException($"{nameof(CreateGenericInterfaceRegistrationProvider)} expects {nameof(genericServiceType)} to be a generic interface type definition; type '{genericServiceType.FullName}' is not a generic type definition");
            }

            if (!genericServiceType.IsInterface) {
                throw new ServiceRegistrationException($"{nameof(CreateGenericInterfaceRegistrationProvider)} expects {nameof(genericServiceType)} to be a generic interface type definition; type '{genericServiceType.FullName}' is not an interface type");
            }

            return implementationType => implementationType.GetInterfaces()
                .Where(serviceType => serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == genericServiceType)
                .Select(serviceType => new ServiceRegistration(serviceType, serviceLifetime));
        }
    }
}
