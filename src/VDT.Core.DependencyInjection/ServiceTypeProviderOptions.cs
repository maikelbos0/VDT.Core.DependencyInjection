using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Options for a method that returns service types for a given implementation type
    /// </summary>
    [Obsolete($"The separate delegates {nameof(ServiceTypeProvider)} and {nameof(ServiceLifetimeProvider)} have been deprecated; they have been replaced by the delegate {nameof(ServiceRegistrationProvider)} that returns both the service type and lifetime. This type will be removed in a future version.")]
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
