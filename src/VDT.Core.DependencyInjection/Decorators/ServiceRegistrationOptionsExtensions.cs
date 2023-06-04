using System;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Extension methods for adding decorators to services to be registered using <see cref="ServiceRegistrationOptions"/>
    /// </summary>
    public static class ServiceRegistrationOptionsExtensions {
        /// <summary>
        /// Use a service registrar that applies decorators to services using the provided setup action
        /// </summary>
        /// <param name="options">The options for registering services</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static ServiceRegistrationOptions UseDecoratorServiceRegistrar(this ServiceRegistrationOptions options, Action<DecoratorOptions> setupAction) 
            => options.UseServiceRegistrar((services, serviceType, implementationType, serviceLifetime) => services.Add(serviceType, implementationType, null, serviceLifetime, setupAction));
    }
}
