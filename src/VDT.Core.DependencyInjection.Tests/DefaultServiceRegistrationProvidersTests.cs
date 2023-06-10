using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Tests.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests {
    public class DefaultServiceRegistrationProvidersTests {
        [Fact]
        public void CreateSingleInterfaceProvider_Returns_ServiceRegistrationProvider_That_Returns_Single_Interface_If_Found() {
            var provider = DefaultServiceRegistrationProviders.CreateSingleInterfaceProvider();

            var serviceRegistration = Assert.Single(provider(typeof(DefaultSingleInterfaceService)));

            Assert.Equal(typeof(ISingleInterfaceService), serviceRegistration.ServiceType);
            Assert.Null(serviceRegistration.ServiceLifetime);
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
            var provider = DefaultServiceRegistrationProviders.CreateSingleInterfaceProvider();

            Assert.Empty(provider(typeof(ImplementationOnlyService)));
        }

        [Fact]
        public void CreateSingleInterfaceProvider_Returns_ServiceRegistrationProvider_That_Returns_No_Services_For_Multiple_Interfaces() {
            var provider = DefaultServiceRegistrationProviders.CreateSingleInterfaceProvider();

            Assert.Empty(provider(typeof(NamedService)));
        }

        [Fact]
        public void CreateInterfaceByNameProvider_Returns_ServiceRegistrationProvider_That_Returns_Interfaces_By_Name() {
            var provider = DefaultServiceRegistrationProviders.CreateInterfaceByNameProvider();

            var serviceRegistration = Assert.Single(provider(typeof(NamedService)));

            Assert.Equal(typeof(INamedService), serviceRegistration.ServiceType);
            Assert.Null(serviceRegistration.ServiceLifetime);
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
            var provider = DefaultServiceRegistrationProviders.CreateInterfaceByNameProvider();

            Assert.Empty(provider(typeof(DefaultSingleInterfaceService)));
        }

        [Fact]
        public void CreateGenericInterfaceRegistrationProvider_Returns_ServiceRegistrationProvider_That_Returns_Correct_Constructed_Generic_Service_Types() {
            var provider = DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(ICommandHandler<>));

            var serviceRegistration = Assert.Single(provider(typeof(StringCommandHandler)));

            Assert.Equal(typeof(ICommandHandler<string>), serviceRegistration.ServiceType);
            Assert.Null(serviceRegistration.ServiceLifetime);
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
            var provider = DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(ICommandHandler<>));

            Assert.Empty(provider(typeof(NamedService)));
        }

        [Fact]
        public void CreateGenericInterfaceRegistrationProvider_Throws_Exception_When_Not_Passing_Unbound_Generic_Type() {
            Assert.Throws<ServiceRegistrationException>(() => DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(IGenericInterface)));
        }

        [Fact]
        public void CreateGenericInterfaceRegistrationProvider_Throws_Exception_When_Not_Passing_Interface_Types() {
            Assert.Throws<ServiceRegistrationException>(() => DefaultServiceRegistrationProviders.CreateGenericInterfaceRegistrationProvider(typeof(CommandHandler<>)));
        }
    }
}
