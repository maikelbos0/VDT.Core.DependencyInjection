﻿using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using VDT.Core.DependencyInjection.Decorators;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class ServiceCollectionExtensionsTests {
        private readonly ServiceCollection services;
        private readonly TestDecorator decorator;

        public ServiceCollectionExtensionsTests() {
            services = new ServiceCollection();
            decorator = new TestDecorator();

            services.AddSingleton(decorator);
        }

        [Fact]
        public async Task AddTransient_Adds_Decorators() {
            services.AddTransient<IServiceCollectionTarget, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Bar", await proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public void AddTransient_Without_Decorators_Does_Not_Register_Proxy() {
            services.AddTransient<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.IsType<ServiceCollectionTarget>(service);
        }

        [Fact]
        public void AddTransient_Throws_Exception_For_Equal_Service_And_Implementation() {
            Assert.Throws<ServiceRegistrationException>(() => services.AddTransient<ServiceCollectionTarget, ServiceCollectionTarget>(options => options.AddDecorator<TestDecorator>()));
        }

        [Fact]
        public void AddTransient_Without_Decorators_Does_Not_Throw_Exception_For_Equal_Service_And_Implementation() {
            services.AddTransient<ServiceCollectionTarget, ServiceCollectionTarget>(options => { });
        }

        [Fact]
        public async Task AddTransient_With_Factory_Adds_Decorators() {
            services.AddTransient<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Foo", await proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public void AddTransient_With_Factory_Without_Decorators_Does_Not_Register_Proxy() {
            services.AddTransient<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.IsType<ServiceCollectionTarget>(service);
        }

        [Fact]
        public void AddTransient_With_Factory_Throws_Exception_For_Equal_Service_And_Implementation() {
            Assert.Throws<ServiceRegistrationException>(() => services.AddTransient<ServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => options.AddDecorator<TestDecorator>()));
        }

        [Fact]
        public void AddTransient_With_Factory_Without_Decorators_Does_Not_Throw_Exception_For_Equal_Service_And_Implementation() {
            services.AddTransient<ServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });
        }

        [Fact]
        public void AddTransient_Always_Returns_New_Object() {
            services.AddTransient<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            var serviceProvider = services.BuildServiceProvider();

            Assert.NotSame(serviceProvider.GetRequiredService<IServiceCollectionTarget>(), serviceProvider.GetRequiredService<IServiceCollectionTarget>());
        }

        [Fact]
        public async Task AddScoped_Adds_Decorators() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Bar", await proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public void AddScoped_Without_Decorators_Does_Not_Register_Proxy() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.IsType<ServiceCollectionTarget>(service);
        }

        [Fact]
        public void AddScoped_Throws_Exception_For_Equal_Service_And_Implementation() {
            Assert.Throws<ServiceRegistrationException>(() => services.AddScoped<ServiceCollectionTarget, ServiceCollectionTarget>(options => options.AddDecorator<TestDecorator>()));
        }

        [Fact]
        public void AddScoped_Without_Decorators_Does_Not_Throw_Exception_For_Equal_Service_And_Implementation() {
            services.AddScoped<ServiceCollectionTarget, ServiceCollectionTarget>(options => { });
        }

        [Fact]
        public async Task AddScoped_With_Factory_Adds_Decorators() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Foo", await proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public void AddScoped_With_Factory_Without_Decorators_Does_Not_Register_Proxy() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.IsType<ServiceCollectionTarget>(service);
        }

        [Fact]
        public void AddScoped_With_Factory_Throws_Exception_For_Equal_Service_And_Implementation() {
            Assert.Throws<ServiceRegistrationException>(() => services.AddScoped<ServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => options.AddDecorator<TestDecorator>()));
        }

        [Fact]
        public void AddScoped_With_Factory_Without_Decorators_Does_Not_Throw_Exception_For_Equal_Service_And_Implementation() {
            services.AddScoped<ServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });
        }

        [Fact]
        public void AddScoped_Returns_Same_Object_Within_Same_Scope() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(scope.ServiceProvider.GetRequiredService<IServiceCollectionTarget>(), scope.ServiceProvider.GetRequiredService<IServiceCollectionTarget>());
            }
        }

        [Fact]
        public void AddScoped_Returns_New_Object_Within_Different_Scopes() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            var serviceProvider = services.BuildServiceProvider();
            IServiceCollectionTarget scopedTarget;

            using (var scope = serviceProvider.CreateScope()) {
                scopedTarget = scope.ServiceProvider.GetRequiredService<IServiceCollectionTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.NotSame(scopedTarget, scope.ServiceProvider.GetRequiredService<IServiceCollectionTarget>());
            }
        }

        [Fact]
        public async Task AddSingleton_Adds_Decorators() {
            services.AddSingleton<IServiceCollectionTarget, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Bar", await proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public void AddSingleton_Without_Decorators_Does_Not_Register_Proxy() {
            services.AddSingleton<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.IsType<ServiceCollectionTarget>(service);
        }

        [Fact]
        public void AddSingleton_Throws_Exception_For_Equal_Service_And_Implementation() {
            Assert.Throws<ServiceRegistrationException>(() => services.AddSingleton<ServiceCollectionTarget, ServiceCollectionTarget>(options => options.AddDecorator<TestDecorator>()));
        }

        [Fact]
        public void AddSingleton_Without_Decorators_Does_Not_Throw_Exception_For_Equal_Service_And_Implementation() {
            services.AddSingleton<ServiceCollectionTarget, ServiceCollectionTarget>(options => { });
        }

        [Fact]
        public async Task AddSingleton_With_Factory_Adds_Decorators() {
            services.AddSingleton<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Foo", await proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public void AddSingleton_With_Factory_Without_Decorators_Does_Not_Register_Proxy() {
            services.AddSingleton<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });

            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.IsType<ServiceCollectionTarget>(service);
        }

        [Fact]
        public void AddSingleton_With_Factory_Throws_Exception_For_Equal_Service_And_Implementation() {
            Assert.Throws<ServiceRegistrationException>(() => services.AddSingleton<ServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => options.AddDecorator<TestDecorator>()));
        }

        [Fact]
        public void AddSingleton_With_Factory_Without_Decorators_Does_Not_Throw_Exception_For_Equal_Service_And_Implementation() {
            services.AddSingleton<ServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });
        }

        [Fact]
        public void AddSingleton_Always_Returns_Same_Object() {
            services.AddSingleton<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            var serviceProvider = services.BuildServiceProvider();
            IServiceCollectionTarget singletonTarget;

            using (var scope = serviceProvider.CreateScope()) {
                singletonTarget = scope.ServiceProvider.GetRequiredService<IServiceCollectionTarget>();
            }

            using (var scope = serviceProvider.CreateScope()) {
                Assert.Same(singletonTarget, scope.ServiceProvider.GetRequiredService<IServiceCollectionTarget>());
            }
        }

        [Fact]
        public async Task Registering_With_Base_Class_Adds_Decorators() {
            services.AddScoped<ServiceCollectionTargetBase, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<ServiceCollectionTargetBase>();

            Assert.Equal("Bar", await proxy.GetValue());

            Assert.Equal(2, decorator.Calls);
        }
    }
}
