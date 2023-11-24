using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using VDT.Core.DependencyInjection.Tests.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceCollectionExtensionsTests {
        [Fact]
        public void AddServices_Uses_ServiceRegistration_ServiceLifetime_If_Provided() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceRegistrationProviders.Add(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)).Select(i => new ServiceRegistration(i, ServiceLifetime.Singleton)));
                options.DefaultServiceLifetime = ServiceLifetime.Scoped;
            });

            Assert.Equal(ServiceLifetime.Singleton, services.Single(s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Uses_DefaultServiceLifetime_If_No_ServiceRegistration_ServiceLifetime_Provided() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceRegistrationProviders.Add(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)).Select(i => new ServiceRegistration(i)));
                options.DefaultServiceLifetime = ServiceLifetime.Singleton;
            });

            Assert.Equal(ServiceLifetime.Singleton, services.Single(s => s.ServiceType == typeof(INamedService)).Lifetime);
        }
        
        [Fact]
        public void AddServices_Uses_ServiceRegistrar_If_Provided() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceRegistrationProviders.Add(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)).Select(i => new ServiceRegistration(i)));
                options.DefaultServiceLifetime = ServiceLifetime.Singleton;
                options.ServiceRegistrar = (services, serviceType, implementationType, serviceLifetime) => services.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped));
            });

            Assert.Equal(ServiceLifetime.Scoped, Assert.Single(services, s => s.ServiceType == typeof(INamedService)).Lifetime);
        }

        [Fact]
        public void AddServices_Creates_ServiceRegistrations_If_No_ServiceRegistrar_Supplied() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceRegistrationProviders.Add(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)).Select(i => new ServiceRegistration(i)));
                options.DefaultServiceLifetime = ServiceLifetime.Scoped;
            });

            var service = Assert.Single(services, s => s.ServiceType == typeof(INamedService));

            Assert.Equal(typeof(NamedService), service.ImplementationType);
            Assert.Equal(ServiceLifetime.Scoped, service.Lifetime);
        }

        [Fact]
        public void AddServices_Adds_Services_From_All_ServiceRegistrationProviders() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
                options.ServiceRegistrationProviders.Add(t => t.GetInterfaces().Where(i => i == typeof(IGenericInterface)).Select(i => new ServiceRegistration(i)));
                options.ServiceRegistrationProviders.Add(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)).Select(i => new ServiceRegistration(i)));
            });

            Assert.Contains(services, s => s.ServiceType == typeof(INamedService));
            Assert.Contains(services, s => s.ServiceType == typeof(IGenericInterface));
        }

        [Fact]
        public void AddServices_Adds_No_Services_When_No_Assemblies_Supplied() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.ServiceRegistrationProviders.Add(t => t.GetInterfaces().Where(i => i != typeof(IGenericInterface)).Select(i => new ServiceRegistration(i)));
            });

            Assert.Empty(services);
        }

        [Fact]
        public void AddServices_Adds_No_Services_When_No_ServiceRegistrationProviders_Supplied() {
            var services = new ServiceCollection();

            services.AddServices(options => {
                options.Assemblies.Add(typeof(NamedService).Assembly);
            });

            Assert.Empty(services);
        }
    }
}
