﻿using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Decorators;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class ServiceRegistrationOptionsExtensionsTests {
        [Theory]
        [InlineData(ServiceLifetime.Transient)]
        [InlineData(ServiceLifetime.Scoped)]
        [InlineData(ServiceLifetime.Singleton)]
        public void UseDecoratorServiceRegistrationMethod_Sets_ServiceRegistrationMethod_To_Decorated_AddService_Method(ServiceLifetime serviceLifetime) {
            var options = new ServiceRegistrationOptions();
            var services = new ServiceCollection();

            services.AddSingleton(new TestDecorator());

            options.UseDecoratorServiceRegistrar(decoratorOptions => decoratorOptions.AddDecorator<TestDecorator>());

            Assert.NotNull(options.ServiceRegistrar);

            options.ServiceRegistrar!(services, typeof(IServiceCollectionTarget), typeof(ServiceCollectionTarget), serviceLifetime);

            var service = Assert.Single(services, service => service.ServiceType == typeof(IServiceCollectionTarget));

            Assert.Equal(serviceLifetime, service.Lifetime);
            Assert.NotNull(service.ImplementationFactory);
            Assert.Equal(serviceLifetime, Assert.Single(services, service => service.ServiceType == typeof(ServiceCollectionTarget)).Lifetime);
        }
    }
}
