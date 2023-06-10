using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Commonly used implementations of the <see cref="ServiceTypeProvider"/> delegate
    /// </summary>
    public static class DefaultServiceRegistrationProviders {
        /// <summary>
        /// Create a service registration provider that returns the interface for implementation types if only a single interface is found; otherwise it returns no service types
        /// </summary>
        /// <returns>A <see cref="ServiceTypeProvider"/> that finds the service interface for an implementation type if only a single interface is found</returns>
        public static ServiceRegistrationProvider CreateSingleInterfaceProvider() => CreateSingleInterfaceProvider(null);

        /// <summary>
        /// Create a service registration provider that returns the interface for implementation types if only a single interface is found; otherwise it returns no service types
        /// </summary>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the service types to register</param>
        /// <returns>A <see cref="ServiceTypeProvider"/> that finds the service interface for an implementation type if only a single interface is found</returns>
        public static ServiceRegistrationProvider CreateSingleInterfaceProvider(ServiceLifetime? serviceLifetime) {
            return implementationType => {
                var serviceTypes = implementationType.GetInterfaces();

                if (serviceTypes.Length == 1) {
                    return serviceTypes.Select(serviceType => new ServiceRegistration(serviceType, serviceLifetime));
                }

                return Enumerable.Empty<ServiceRegistration>();
            };
        }

        /// <summary>
        /// Create a service registration provider that returns all interfaces that match the implementation type name with an "I" prefix; e.g. MyService and IMyService
        /// </summary>
        /// <returns>A <see cref="ServiceTypeProvider"/> that finds any service interfaces that match the name of an implementation type</returns>
        public static ServiceRegistrationProvider CreateInterfaceByNameProvider() => CreateInterfaceByNameProvider(null);

        /// <summary>
        /// Create a service registration provider that returns all interfaces that match the implementation type name with an "I" prefix; e.g. MyService and IMyService
        /// </summary>
        /// <param name="serviceLifetime">The <see cref="ServiceLifetime"/> for the service types to register</param>
        /// <returns>A <see cref="ServiceTypeProvider"/> that finds any service interfaces that match the name of an implementation type</returns>
        public static ServiceRegistrationProvider CreateInterfaceByNameProvider(ServiceLifetime? serviceLifetime) {
            return implementationType => implementationType.GetInterfaces()
                .Where(serviceType => serviceType.Name == $"I{implementationType.Name}")
                .Select(serviceType => new ServiceRegistration(serviceType, serviceLifetime));
        }

        /// <summary>
        /// Create a service registration provider that finds all implementations of a generic interface
        /// </summary>
        /// <param name="genericServiceType">The generic interface type definition to match implementation types to</param>
        /// <returns>A <see cref="ServiceTypeProvider"/> that finds any matching constructed service types for an implementation type</returns>
        public static ServiceRegistrationProvider CreateGenericInterfaceRegistrationProvider(Type genericServiceType) => CreateGenericInterfaceRegistrationProvider(genericServiceType, null);

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
