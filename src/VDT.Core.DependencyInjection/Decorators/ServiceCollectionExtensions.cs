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
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.Add<TService, TImplementation>(ServiceLifetime.Transient, setupAction);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">This type parameter is obsolete; <typeparamref name="TImplementation"/> will be used for registration of the implementation</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
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
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        public static IServiceCollection AddTransient<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.Add<TService, TImplementation>(implementationFactory, ServiceLifetime.Transient, setupAction);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">This type parameter is obsolete; <typeparamref name="TImplementation"/> will be used for registration of the implementation</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
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
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.Add<TService, TImplementation>(ServiceLifetime.Scoped, setupAction);
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">This type parameter is obsolete; <typeparamref name="TImplementation"/> will be used for registration of the implementation</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
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
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        public static IServiceCollection AddScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.Add<TService, TImplementation>(implementationFactory, ServiceLifetime.Scoped, setupAction);

        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">This type parameter is obsolete; <typeparamref name="TImplementation"/> will be used for registration of the implementation</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
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
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.Add<TService, TImplementation>(ServiceLifetime.Singleton, setupAction);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">This type parameter is obsolete; <typeparamref name="TImplementation"/> will be used for registration of the implementation</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
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
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        public static IServiceCollection AddSingleton<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.Add<TService, TImplementation>(implementationFactory, ServiceLifetime.Singleton, setupAction);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementationService">This type parameter is obsolete; <typeparamref name="TImplementation"/> will be used for registration of the implementation</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        [Obsolete($"This method has been deprecated because {nameof(TImplementation)} has replaced {nameof(TImplementationService)} as the type under which implementations are registered. It will be removed in a future version.")]
        public static IServiceCollection AddSingleton<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return services.AddSingleton<TService, TImplementation>(implementationFactory, setupAction);
        }

        /// <summary>
        /// Adds a service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        public static IServiceCollection Add<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.Add(typeof(TService), typeof(TImplementation), null, lifetime, setupAction);
        }

        /// <summary>
        /// Adds a service of the type specified in <typeparamref name="TService"/> with an implementation type specified in <typeparamref name="TImplementation"/> to the specified <see cref="IServiceCollection"/>
        /// using the provided factory
        /// </summary>
        /// <typeparam name="TService">The type of the service to add</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime"/> of the service</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        /// <remarks>
        /// If decorators will be applied, the type specified in <typeparamref name="TImplementation"/> needs to be different from the type specified in <typeparamref name="TService"/> since the implementation will be
        /// registered under <typeparamref name="TImplementation"/> and the decorator proxy will be registered under <typeparamref name="TService"/>
        /// </remarks>
        public static IServiceCollection Add<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, ServiceLifetime lifetime, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementation : class, TService {

            return services.Add(typeof(TService), typeof(TImplementation), implementationFactory, lifetime, setupAction);
        }

        internal static IServiceCollection Add(this IServiceCollection services, Type serviceType, Type implementationType, Func<IServiceProvider, object>? implementationFactory, ServiceLifetime lifetime, Action<DecoratorOptions> setupAction) {
            var options = GetDecoratorOptions(serviceType, implementationType, setupAction);

            if (options.Policies.Any()) {
                VerifyRegistration(serviceType, implementationType);

                var proxyFactory = GetDecoratedProxyFactory(serviceType, implementationType, options);

                services.Add(new ServiceDescriptor(serviceType, proxyFactory, lifetime));
                services.Add(implementationType, implementationType, implementationFactory, lifetime);
            }
            else {
                services.Add(serviceType, implementationType, implementationFactory, lifetime);
            }

            return services;
        }

        private static void Add(this IServiceCollection services, Type serviceType, Type implementationType, Func<IServiceProvider, object>? implementationFactory, ServiceLifetime lifetime) {
            if (implementationFactory == null) {
                services.Add(new ServiceDescriptor(serviceType, implementationType, lifetime));
            }
            else {
                services.Add(new ServiceDescriptor(serviceType, implementationFactory, lifetime));
            }
        }

        private static void VerifyRegistration(Type serviceType, Type implementationType) {
            if (serviceType == implementationType) {
                throw new ServiceRegistrationException($"Implementation type '{serviceType.FullName}' can not be equal to service type '{implementationType.FullName}'.");
            }
        }

        private static DecoratorOptions GetDecoratorOptions(Type serviceType, Type implementationType, Action<DecoratorOptions> setupAction) {
            var options = new DecoratorOptions(serviceType, implementationType);
            setupAction(options);

            return options;
        }

        private static Func<IServiceProvider, object> GetDecoratedProxyFactory(Type serviceType, Type implementationType, DecoratorOptions options) {
            var generator = new Castle.DynamicProxy.ProxyGenerator();
            var isInterface = serviceType.IsInterface;
            object?[]? constructorArguments = null;

            if (!isInterface) {
                // We need to supply constructor arguments; the actual content does not matter since only overridable methods will be called
                var constructor = serviceType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .FirstOrDefault(c => !c.IsPrivate);

                if (constructor == null) {
                    throw new ServiceRegistrationException($"Service type '{serviceType.FullName}' has no accessible constructor; class service types require at least one public or protected constructor.");
                }

                constructorArguments = Enumerable.Range(0, constructor.GetParameters().Length)
                    .Select(i => (object?)null)
                    .ToArray();
            }

            return serviceProvider => {
                var target = serviceProvider.GetRequiredService(implementationType);
                var decorators = options.Policies.Select(p => new DecoratorInterceptor(p.GetDecorator(serviceProvider), p.Predicate)).ToArray();

                if (isInterface) {
                    return generator.CreateInterfaceProxyWithTarget(serviceType, target, decorators);
                }
                else {
                    return generator.CreateClassProxyWithTarget(serviceType, target, constructorArguments, decorators);
                }
            };
        }
    }
}
