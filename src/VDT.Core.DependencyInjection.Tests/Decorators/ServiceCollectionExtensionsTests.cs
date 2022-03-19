﻿using Microsoft.Extensions.DependencyInjection;
using System.Linq;
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
        public async Task Add_Adds_DecoratorInjectors() {
            services.Add(
                typeof(IServiceCollectionTarget),
                typeof(ServiceCollectionTarget),
                ServiceLifetime.Singleton,
                options => {
                    options.AddDecorator<TestDecorator>();
                    options.AddDecorator<TestDecorator>();
                }
            );

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Bar", await proxy.GetValue());
            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public async Task Add_With_Factory_Adds_DecoratorInjectors() {
            services.Add(
                typeof(IServiceCollectionTarget),
                typeof(ServiceCollectionTarget),
                serviceProvider => new ServiceCollectionTarget {
                    Value = "Foo"
                },
                ServiceLifetime.Singleton,
                options => {
                    options.AddDecorator<TestDecorator>();
                    options.AddDecorator<TestDecorator>();
                }
            );

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Foo", await proxy.GetValue());
            Assert.Equal(2, decorator.Calls);
        }

        // TODO clean up tests
        [Fact]
        public async Task AddTransient_Adds_DecoratorInjectors() {
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
        public async Task AddTransient_With_ImplementationTarget_Adds_DecoratorInjectors() {
            services.AddTransient<IServiceCollectionTarget, IServiceCollectionTargetImplementation, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Bar", await proxy.GetValue());
            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public async Task AddTransient_With_Factory_Adds_DecoratorInjectors() {
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
        public async Task AddTransient_With_ImplementationTarget_And_Factory_Adds_DecoratorInjectors() {
            services.AddTransient<IServiceCollectionTarget, IServiceCollectionTargetImplementation, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
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
        public void AddTransient_Registers_Services_As_Transient() {
            services.AddTransient<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            Assert.All(services.Where(s => !typeof(IDecorator).IsAssignableFrom(s.ServiceType)), s => Assert.Equal(ServiceLifetime.Transient, s.Lifetime));
        }

        [Fact]
        public void AddTransient_With_Factory_Registers_Services_As_Transient() {
            services.AddTransient<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });

            Assert.All(services.Where(s => !typeof(IDecorator).IsAssignableFrom(s.ServiceType)), s => Assert.Equal(ServiceLifetime.Transient, s.Lifetime));
        }

        [Fact]
        public async Task AddScoped_Adds_DecoratorInjectors() {
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
        public async Task AddScoped_With_ImplementationTarget_Adds_DecoratorInjectors() {
            services.AddScoped<IServiceCollectionTarget, IServiceCollectionTargetImplementation, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Bar", await proxy.GetValue());
            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public async Task AddScoped_With_Factory_Adds_DecoratorInjectors() {
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
        public async Task AddScoped_With_ImplementationTarget_And_Factory_Adds_DecoratorInjectors() {
            services.AddScoped<IServiceCollectionTarget, IServiceCollectionTargetImplementation, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
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
        public void AddScoped_Registers_Services_As_Scoped() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            Assert.All(services.Where(s => !typeof(IDecorator).IsAssignableFrom(s.ServiceType)), s => Assert.Equal(ServiceLifetime.Scoped, s.Lifetime));
        }

        [Fact]
        public void AddScoped_With_Factory_Registers_Services_As_Scoped() {
            services.AddScoped<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });

            Assert.All(services.Where(s => !typeof(IDecorator).IsAssignableFrom(s.ServiceType)), s => Assert.Equal(ServiceLifetime.Scoped, s.Lifetime));
        }

        [Fact]
        public async Task AddSingleton_Adds_DecoratorInjectors() {
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
        public async Task AddSingleton_With_ImplementationTarget_Adds_DecoratorInjectors() {
            services.AddSingleton<IServiceCollectionTarget, IServiceCollectionTargetImplementation, ServiceCollectionTarget>(options => {
                options.AddDecorator<TestDecorator>();
                options.AddDecorator<TestDecorator>();
            });

            var serviceProvider = services.BuildServiceProvider();
            var proxy = serviceProvider.GetRequiredService<IServiceCollectionTarget>();

            Assert.Equal("Bar", await proxy.GetValue());
            Assert.Equal(2, decorator.Calls);
        }

        [Fact]
        public async Task AddSingleton_With_Factory_Adds_DecoratorInjectors() {
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
        public async Task AddSingleton_With_ImplementationTarget_And_Factory_Adds_DecoratorInjectors() {
            services.AddSingleton<IServiceCollectionTarget, IServiceCollectionTargetImplementation, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
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
        public void AddSingleton_Registers_Services_As_Singleton() {
            services.AddSingleton<IServiceCollectionTarget, ServiceCollectionTarget>(options => { });

            Assert.All(services.Where(s => !typeof(IDecorator).IsAssignableFrom(s.ServiceType)), s => Assert.Equal(ServiceLifetime.Singleton, s.Lifetime));
        }

        [Fact]
        public void AddSingleton_With_Factory_Registers_Services_As_Singleton() {
            services.AddSingleton<IServiceCollectionTarget, ServiceCollectionTarget>(serviceProvider => new ServiceCollectionTarget {
                Value = "Foo"
            }, options => { });

            Assert.All(services.Where(s => !typeof(IDecorator).IsAssignableFrom(s.ServiceType)), s => Assert.Equal(ServiceLifetime.Singleton, s.Lifetime));
        }

        [Fact]
        public async Task Registering_With_Base_Class_Adds_DecoratorInjectors() {
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
