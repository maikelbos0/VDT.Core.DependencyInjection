using Microsoft.Extensions.DependencyInjection;
using System;

namespace VDT.Core.DependencyInjection.Attributes {
    /// <summary>
    /// Marks a service to be registered as scoped when calling <see cref="DependencyInjection.ServiceCollectionExtensions.AddServices(IServiceCollection, Action{ServiceRegistrationOptions})"/>
    /// with <see cref="ServiceRegistrationOptionsExtensions.AddAttributeServiceRegistrationProviders(ServiceRegistrationOptions)"/> called on the <see cref="ServiceRegistrationOptions"/> builder action
    /// </summary>
    /// <remarks>Services registered with this attribute can not be decorated</remarks>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ScopedAttribute : Attribute, IAttribute {
        /// <summary>
        /// The lifetime of services marked with this attribute is <see cref="ServiceLifetime.Scoped"/>
        /// </summary>
        public ServiceLifetime ServiceLifetime => ServiceLifetime.Scoped;
    }
}
