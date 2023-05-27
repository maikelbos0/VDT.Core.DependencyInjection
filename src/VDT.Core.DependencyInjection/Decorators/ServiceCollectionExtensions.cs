using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    /// <summary>
    /// Extension methods for adding decorated services to an <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionExtensions {
        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services
                .AddProxy<TService, TImplementation>((services, proxyFactory) => services.AddTransient(proxyFactory), setupAction)
                .AddTransient<TImplementation, TImplementation>();
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        [Obsolete($"This method has been deprecated because {nameof(TImplementation)} has replaced {nameof(TImplementationService)} as the type under which implementations are registered. It will be removed in a future version.")]
        public static IServiceCollection AddTransient<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services.AddTransient<TService, TImplementation>(setupAction);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services
                .AddProxy<TService, TImplementation>((services, proxyFactory) => services.AddTransient(proxyFactory), setupAction)
                .AddTransient<TImplementation, TImplementation>(implementationFactory);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        [Obsolete($"This method has been deprecated because {nameof(TImplementation)} has replaced {nameof(TImplementationService)} as the type under which implementations are registered. It will be removed in a future version.")]
        public static IServiceCollection AddTransient<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services.AddTransient<TService, TImplementation>(implementationFactory, setupAction);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services
                .AddProxy<TService, TImplementation>((services, proxyFactory) => services.AddScoped(proxyFactory), setupAction)
                .AddScoped<TImplementation, TImplementation>();
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        [Obsolete($"This method has been deprecated because {nameof(TImplementation)} has replaced {nameof(TImplementationService)} as the type under which implementations are registered. It will be removed in a future version.")]
        public static IServiceCollection AddScoped<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services.AddScoped<TService, TImplementation>(setupAction);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services
                .AddProxy<TService, TImplementation>((services, proxyFactory) => services.AddScoped(proxyFactory), setupAction)
                .AddScoped<TImplementation, TImplementation>(implementationFactory);

        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        [Obsolete($"This method has been deprecated because {nameof(TImplementation)} has replaced {nameof(TImplementationService)} as the type under which implementations are registered. It will be removed in a future version.")]
        public static IServiceCollection AddScoped<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services.AddScoped<TService, TImplementation>(implementationFactory, setupAction);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services
                .AddProxy<TService, TImplementation>((services, proxyFactory) => services.AddSingleton(proxyFactory), setupAction)
                .AddSingleton<TImplementation, TImplementation>();
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        [Obsolete($"This method has been deprecated because {nameof(TImplementation)} has replaced {nameof(TImplementationService)} as the type under which implementations are registered. It will be removed in a future version.")]
        public static IServiceCollection AddSingleton<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services.AddSingleton<TService, TImplementation>(setupAction);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services
                .AddProxy<TService, TImplementation>((services, proxyFactory) => services.AddSingleton(proxyFactory), setupAction)
                .AddSingleton(implementationFactory);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">The type with which the implementation will be registered and resolved</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>The type specified in <typeparamref name="TImplementationService"/> needs to be different from the type specified in <typeparamref name="TService"/> since it will be used to resolve the implementation from the service provider</remarks>
        [Obsolete($"This method has been deprecated because {nameof(TImplementation)} has replaced {nameof(TImplementationService)} as the type under which implementations are registered. It will be removed in a future version.")]
        public static IServiceCollection AddSingleton<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services.AddSingleton<TService, TImplementation>(implementationFactory, setupAction);
        }

        private static IServiceCollection AddProxy<TService, TImplementation>(this IServiceCollection services, Func<IServiceCollection, Func<IServiceProvider, TService>, IServiceCollection> registerProxy, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            VerifyRegistration<TService, TImplementation>();

            var options = GetDecoratorOptions<TService, TImplementation>(setupAction);
            var proxyFactory = GetDecoratedProxyFactory<TService, TImplementation>(options);

            return registerProxy(services, proxyFactory);
        }

        private static void VerifyRegistration<TService, TImplementationService>()
            where TService : class
            where TImplementationService : class, TService {

            if (typeof(TService) == typeof(TImplementationService)) {
                throw new ServiceRegistrationException($"Implementation service type '{typeof(TImplementationService).FullName}' can not be equal to service type '{typeof(TService).FullName}'.");
            }
        }

        private static DecoratorOptions GetDecoratorOptions<TService, TImplementation>(Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            var options = new DecoratorOptions(typeof(TService), typeof(TImplementation));
            setupAction(options);

            return options;
        }

        private static Func<IServiceProvider, TService> GetDecoratedProxyFactory<TService, TImplementation>(DecoratorOptions options)
            where TService : class
            where TImplementation : class, TService {

            var generator = new Castle.DynamicProxy.ProxyGenerator();
            var isInterface = typeof(TService).IsInterface;
            object?[]? constructorArguments = null;

            if (!isInterface) {
                // We need to supply constructor arguments; the actual content does not matter since only overridable methods will be called
                var constructor = typeof(TService).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .FirstOrDefault(c => !c.IsPrivate);

                if (constructor == null) {
                    throw new ServiceRegistrationException($"Service type '{typeof(TService).FullName}' has no accessible constructor; class service types require at least one public or protected constructor.");
                }

                constructorArguments = Enumerable.Range(0, constructor.GetParameters().Length)
                    .Select(i => (object?)null)
                    .ToArray();
            }

            return serviceProvider => {
                var target = serviceProvider.GetRequiredService<TImplementation>();
                var decorators = options.Policies.Select(p => new DecoratorInterceptor(p.GetDecorator(serviceProvider), p.Predicate)).ToArray();

                if (isInterface) {
                    return generator.CreateInterfaceProxyWithTarget<TService>(target, decorators);
                }
                else {
                    return (TService)generator.CreateClassProxyWithTarget(typeof(TService), target, constructorArguments, decorators);
                }
            };
        }
    }
}
