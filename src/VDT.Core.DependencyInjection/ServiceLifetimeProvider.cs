using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Signature for methods that return a service lifetime for a given service and implementation type
    /// </summary>
    /// <param name="serviceType">The service type</param>
    /// <param name="implementationType">The implementation type</param>
    /// <returns>A service lifetime to apply to this service registration or null if the lifetime could not be provided</returns>
    [Obsolete($"The separate delegates {nameof(ServiceTypeProvider)} and {nameof(ServiceLifetimeProvider)} have been deprecated; they have been replaced by the delegate {nameof(ServiceRegistrationProvider)} that returns both the service type and lifetime. This delegate will be removed in a future version.")]
    public delegate ServiceLifetime? ServiceLifetimeProvider(Type serviceType, Type implementationType);
}
