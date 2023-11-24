using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using VDT.Core.DependencyInjection.Tests.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class ServiceRegistrationOptionsTests {
        [Fact]
        public void AddAssembly_Adds_Assembly() {
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddAssembly(typeof(NamedService).Assembly));
            Assert.Equal(typeof(NamedService).Assembly, Assert.Single(options.Assemblies));
        }

        [Fact]
        public void AddAssemblies_Adds_Assemblies_Enumerable() {
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddAssemblies(new[] { typeof(NamedService).Assembly, typeof(ServiceRegistrationOptionsTests).Assembly }));
            Assert.Equal(new[] { typeof(NamedService).Assembly, typeof(ServiceRegistrationOptionsTests).Assembly }, options.Assemblies);
        }

        [Fact]
        public void AddAssemblies_Adds_Assemblies_Param_Array() {
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddAssemblies(typeof(NamedService).Assembly, typeof(ServiceRegistrationOptionsTests).Assembly));
            Assert.Equal(new[] { typeof(NamedService).Assembly, typeof(ServiceRegistrationOptionsTests).Assembly }, options.Assemblies);
        }

        [Fact]
        public void AddAssemblies_Adds_Assemblies_EntryAssembly_FilterPredicate_ScanPredicate() {
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddAssemblies(
                typeof(ServiceRegistrationOptionsTests).Assembly,
                a => !(a.FullName?.StartsWith("VDT.Core.DependencyInjection.Tests.Targets") ?? false),
                a => a.FullName?.StartsWith("VDT.Core.DependencyInjection") ?? false
            ));
            Assert.Equal(new System.Reflection.Assembly[] {
                typeof(ServiceRegistrationOptionsTests).Assembly,
                typeof(ServiceRegistrationOptions).Assembly,
                typeof(Decorators.Targets.DecoratorOptionsTarget).Assembly,
                typeof(Attributes.Targets.AttributeServiceInterfaceTarget).Assembly
            }, options.Assemblies);
        }

        [Fact]
        public void AddAssemblies_Adds_Assemblies_EntryAssembly_AssemblyPrefix() {
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddAssemblies(typeof(ServiceRegistrationOptionsTests).Assembly, "VDT.Core.DependencyInjection"));
            Assert.Contains(options.Assemblies, a => a.FullName?.StartsWith("VDT.Core.DependencyInjection") ?? false);
            Assert.DoesNotContain(options.Assemblies, a => !(a.FullName?.StartsWith("VDT.Core.DependencyInjection") ?? false));
        }

        [Fact]
        public void AddServiceRegistrationProvider_Adds_ServiceRegistrationProvider() {
            ServiceRegistrationProvider serviceRegistrationProvider = implementationType => Enumerable.Empty<ServiceRegistration>();
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.AddServiceRegistrationProvider(serviceRegistrationProvider));
            Assert.Equal(serviceRegistrationProvider, Assert.Single(options.ServiceRegistrationProviders));
        }

        [Fact]
        public void UseDefaultServiceLifetime_Sets_DefaultServiceLifetime() {
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.UseDefaultServiceLifetime(ServiceLifetime.Singleton));
            Assert.Equal(ServiceLifetime.Singleton, options.DefaultServiceLifetime);
        }

        [Fact]
        public void UseServiceRegistrar_Sets_ServiceRegistrar() {
            ServiceRegistrar serviceRegistrar = (services, serviceType, implementationType, serviceLifetime) => { };
            var options = new ServiceRegistrationOptions();

            Assert.Equal(options, options.UseServiceRegistrar(serviceRegistrar));
            Assert.Equal(serviceRegistrar, options.ServiceRegistrar);
        }
    }
}
