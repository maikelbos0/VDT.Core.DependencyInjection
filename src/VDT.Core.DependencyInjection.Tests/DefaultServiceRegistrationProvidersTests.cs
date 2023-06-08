using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class DefaultServiceRegistrationProvidersTests {
        [Fact]
        public void CreateSingleInterfaceProvider_Returns_ServiceRegistrationProvider_That_Returns_Single_Interface_If_Found() {
            var provider = DefaultServiceRegistrationProviders.CreateSingleInterfaceProvider(null);

            Assert.Equal(typeof(ISingleInterfaceService), Assert.Single(provider(typeof(DefaultSingleInterfaceService))).ServiceType);
        }

        [Theory]
        [InlineData(ServiceLifetime.Scoped)]
        [InlineData(null)]
        public void CreateSingleInterfaceProvider_Returns_ServiceRegistrationProvider_That_Returns_Correct_ServiceLifetime(ServiceLifetime? serviceLifetime) {
            var provider = DefaultServiceRegistrationProviders.CreateSingleInterfaceProvider(serviceLifetime);

            Assert.Equal(serviceLifetime, Assert.Single(provider(typeof(DefaultSingleInterfaceService))).ServiceLifetime);
        }

        [Fact]
        public void CreateSingleInterfaceProvider_Returns_ServiceRegistrationProvider_That_Returns_No_Services_For_No_Interfaces() {
            var provider = DefaultServiceRegistrationProviders.CreateSingleInterfaceProvider(null);

            Assert.Empty(provider(typeof(ImplementationOnlyService)));
        }

        [Fact]
        public void CreateSingleInterfaceProvider_Returns_ServiceRegistrationProvider_That_Returns_No_Services_For_Multiple_Interfaces() {
            var provider = DefaultServiceRegistrationProviders.CreateSingleInterfaceProvider(null);

            Assert.Empty(provider(typeof(NamedService)));
        }

        [Fact]
        public void CreateInterfaceByNameProvider_Returns_ServiceRegistrationProvider_That_Returns_Interfaces_By_Name() {
            var provider = DefaultServiceRegistrationProviders.CreateInterfaceByNameProvider(null);

            Assert.Equal(typeof(INamedService), Assert.Single(provider(typeof(NamedService))).ServiceType);
        }

        [Theory]
        [InlineData(ServiceLifetime.Scoped)]
        [InlineData(null)]
        public void CreateInterfaceByNameProvider_Returns_ServiceRegistrationProvider_That_Returns_Correct_ServiceLifetime(ServiceLifetime? serviceLifetime) {
            var provider = DefaultServiceRegistrationProviders.CreateInterfaceByNameProvider(serviceLifetime);

            Assert.Equal(serviceLifetime, Assert.Single(provider(typeof(NamedService))).ServiceLifetime);
        }

        [Fact]
        public void CreateInterfaceByNameProvider_Returns_ServiceRegistrationProvider_That_Returns_No_Services_For_No_Correctly_Named_Interfaces() {
            var provider = DefaultServiceRegistrationProviders.CreateInterfaceByNameProvider(null);

            Assert.Empty(provider(typeof(DefaultSingleInterfaceService)));
        }

        [Fact]
        public void CreateGenericInterfaceRegistrationProvider_Returns_ServiceRegistrationProvider_That_Returns_Correct_Constructed_Generic_Service_Types() {
            var provider = DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(ICommandHandler<>), null);

            Assert.Equal(typeof(ICommandHandler<string>), Assert.Single(provider(typeof(StringCommandHandler))).ServiceType);
        }

        [Theory]
        [InlineData(ServiceLifetime.Scoped)]
        [InlineData(null)]
        public void CreateGenericInterfaceRegistrationProvider_Returns_ServiceRegistrationProvider_That_Returns_Correct_ServiceLifetime(ServiceLifetime? serviceLifetime) {
            var provider = DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(ICommandHandler<>), serviceLifetime);

            Assert.Equal(serviceLifetime, Assert.Single(provider(typeof(StringCommandHandler))).ServiceLifetime);
        }

        [Fact]
        public void CreateGenericInterfaceRegistrationProvider_Returns_ServiceRegistrationProvider_That_Returns_No_Services_For_Not_Generic_Interface() {
            var provider = DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(ICommandHandler<>), null);

            Assert.Empty(provider(typeof(NamedService)));
        }

        [Fact]
        public void CreateGenericInterfaceRegistrationProvider_Throws_Exception_When_Not_Passing_Unbound_Generic_Type() {
            Assert.Throws<ServiceRegistrationException>(() => DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(IGenericInterface), null));
        }

        [Fact]
        public void CreateGenericInterfaceRegistrationProvider_Throws_Exception_When_Not_Passing_Interface_Types() {
            Assert.Throws<ServiceRegistrationException>(() => DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(CommandHandler<>), null));
        }
    }
}
