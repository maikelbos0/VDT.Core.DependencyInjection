using System;
using System.Collections.Generic;
using System.Linq;

namespace VDT.Core.DependencyInjection.Attributes {
    /// <summary>
    /// Extension methods for adding service registration providers for services marked with attributes to services to be registered using <see cref="ServiceRegistrationOptions"/>
    /// </summary>
    public static class ServiceRegistrationOptionsExtensions {
        /// <summary>
        /// Add service registration providers that find and use <see cref="TransientServiceAttribute"/>, <see cref="ScopedServiceAttribute"/>, <see cref="SingletonServiceAttribute"/>,
        /// <see cref="TransientServiceImplementationAttribute"/>, <see cref="ScopedServiceImplementationAttribute"/> or <see cref="SingletonServiceImplementationAttribute"/> to register services
        /// </summary>
        /// <param name="options">The options for registering services</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static ServiceRegistrationOptions AddAttributeServiceRegistrationProviders(this ServiceRegistrationOptions options) {
            // Attributes on implementation only types
            options.AddServiceRegistrationProvider(
                implementationType => implementationType.GetCustomAttributes(typeof(IAttribute), false)
                    .Cast<IAttribute>()
                    .Select(attribute => new ServiceRegistration(implementationType, attribute.ServiceLifetime))
            );

            // Attributes on implementation types
            options.AddServiceRegistrationProvider(
                implementationType => implementationType.GetCustomAttributes(typeof(IServiceImplementationAttribute), false)
                    .Cast<IServiceImplementationAttribute>()
                    .Select(attribute => new ServiceRegistration(attribute.ServiceType, attribute.ServiceLifetime))
            );

            // Attributes on service interface types
            options.AddServiceRegistrationProvider(
                implementationType => implementationType.GetInterfaces()
                    .SelectMany(serviceType => serviceType.GetCustomAttributes(typeof(IServiceAttribute), false).Cast<IServiceAttribute>().Select(attribute => new ServiceRegistration(serviceType, attribute.ServiceLifetime)))
            );

            // Attributes on service class types
            options.AddServiceRegistrationProvider(
                implementationType => {
                    var currentServiceType = implementationType;
                    var serviceTypes = new List<Type>();

                    do {
                        serviceTypes.Add(currentServiceType);
                        currentServiceType = currentServiceType.BaseType;
                    }
                    while (currentServiceType != null);

                    return serviceTypes.SelectMany(serviceType => serviceType.GetCustomAttributes(typeof(IServiceAttribute), false).Cast<IServiceAttribute>().Select(attribute => new ServiceRegistration(serviceType, attribute.ServiceLifetime)));
                }
            );

            return options;
        }
    }
}
