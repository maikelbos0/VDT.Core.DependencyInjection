using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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

        // TODO remove or properly implement below tests
        [Fact]
        public void TestConstructTypeAndResolveFromProvider() {
            var services = new ServiceCollection();

            AssemblyName asmname = new AssemblyName();
            asmname.Name = "assemfilename";
            AssemblyBuilder asmbuild = AssemblyBuilder.DefineDynamicAssembly(asmname, AssemblyBuilderAccess.Run);
            ModuleBuilder modbuild = asmbuild.DefineDynamicModule("modulename");
            TypeBuilder typebuild1 = modbuild.DefineType("typename", TypeAttributes.Class | TypeAttributes.Public, typeof(TestClass));

            var ctor = typeof(TestClass).GetConstructors().Single();
            var parameters = ctor.GetParameters();
            var ctorbuilder = typebuild1.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameters.Select(p => p.ParameterType).ToArray());

            var emitter = ctorbuilder.GetILGenerator();

            emitter.Emit(OpCodes.Ldarg_0);
            for (var i = 1; i <= parameters.Length; ++i) {
                emitter.Emit(OpCodes.Ldarg, i);
            }
            emitter.Emit(OpCodes.Call, ctor);
            emitter.Emit(OpCodes.Ret);

            var type = typebuild1.CreateType()!;


            services.AddTransient<Service>();
            services.AddTransient(type);

            var provider = services.BuildServiceProvider();

            var x = provider.GetService(type);

            Assert.NotNull(x);
            var x1 = Assert.IsAssignableFrom<TestClass>(x);
            Assert.NotNull(x1.Service);
        }

        [Fact]
        public void TestConstructTypeAsDecorated() {
            var services = new ServiceCollection();
            var interceptor = new TestInterceptor();
            var generator = new ProxyGenerator();

            AssemblyName asmname = new AssemblyName();
            asmname.Name = "assemfilename";
            AssemblyBuilder asmbuild = AssemblyBuilder.DefineDynamicAssembly(asmname, AssemblyBuilderAccess.Run);
            ModuleBuilder modbuild = asmbuild.DefineDynamicModule("modulename");
            TypeBuilder typebuild1 = modbuild.DefineType("typename", TypeAttributes.Class | TypeAttributes.Public, typeof(TestClass));

            var ctor = typeof(TestClass).GetConstructors().Single();
            var parameters = ctor.GetParameters();
            var ctorbuilder = typebuild1.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameters.Select(p => p.ParameterType).ToArray());

            var emitter = ctorbuilder.GetILGenerator();

            emitter.Emit(OpCodes.Ldarg_0);
            for (var i = 1; i <= parameters.Length; ++i) {
                emitter.Emit(OpCodes.Ldarg, i);
            }
            emitter.Emit(OpCodes.Call, ctor);
            emitter.Emit(OpCodes.Ret);

            var type = typebuild1.CreateType()!;

            services.AddSingleton(interceptor);
            services.AddTransient<Service>();
            services.AddTransient(typeof(TestClass), serviceProvider => {
                var constructorArguments = parameters
                    .Select(p => serviceProvider.GetRequiredService(p.ParameterType))
                    .ToArray();
                var impl = ctor.Invoke(constructorArguments);

                return generator.CreateClassProxyWithTarget(typeof(TestClass), impl, constructorArguments, serviceProvider.GetRequiredService<TestInterceptor>());
            });

            var provider = services.BuildServiceProvider();

            var x = provider.GetRequiredService<TestClass>();

            Assert.Equal("Foo", x.VirtualTest());
            Assert.Equal(1, interceptor.Count);
        }
    }


    // TODO remove or move test classes when implemented properly
    public class ServiceProviderStatus<TService> {
        public bool IsRequested { get; set; }
    }

    public class TestClass {
        public Service Service { get; }

        public TestClass(Service service) {
            Service = service;
        }

        public virtual string VirtualTest() => "Foo";
    }

    public class TestInterceptor : IInterceptor {
        public int Count { get; set; }

        public void Intercept(IInvocation invocation) {
            Count++;
            invocation.Proceed();
        }
    }

    public class Service { }
}
