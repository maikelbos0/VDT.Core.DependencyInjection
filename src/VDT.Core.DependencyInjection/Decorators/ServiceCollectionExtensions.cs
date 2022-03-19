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

            return services.AddTransient<TService, TImplementation, TImplementation>(setupAction);
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
        public static IServiceCollection AddTransient<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return Add(services, typeof(TService), typeof(TImplementationService), typeof(TImplementation), ServiceLifetime.Transient, setupAction);
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

            return services.AddTransient<TService, TImplementation, TImplementation>(implementationFactory, setupAction);
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
        public static IServiceCollection AddTransient<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return Add(services, typeof(TService), typeof(TImplementationService), typeof(TImplementation), implementationFactory, ServiceLifetime.Transient, setupAction);
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

            return services.AddScoped<TService, TImplementation, TImplementation>(setupAction);
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
        public static IServiceCollection AddScoped<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return Add(services, typeof(TService), typeof(TImplementationService), typeof(TImplementation), ServiceLifetime.Scoped, setupAction);
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

            return services.AddScoped<TService, TImplementation, TImplementation>(implementationFactory, setupAction);
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
        public static IServiceCollection AddScoped<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return Add(services, typeof(TService), typeof(TImplementationService), typeof(TImplementation), implementationFactory, ServiceLifetime.Scoped, setupAction);
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

            return services.AddSingleton<TService, TImplementation, TImplementation>(setupAction);
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
        public static IServiceCollection AddSingleton<TService, TImplementationService, TImplementation>(this IServiceCollection services, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return Add(services, typeof(TService), typeof(TImplementationService), typeof(TImplementation), ServiceLifetime.Singleton, setupAction);
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

            return services.AddSingleton<TService, TImplementation, TImplementation>(implementationFactory, setupAction);
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
        public static IServiceCollection AddSingleton<TService, TImplementationService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> implementationFactory, Action<DecoratorOptions> setupAction)
            where TService : class
            where TImplementationService : class, TService
            where TImplementation : class, TImplementationService {

            return Add(services, typeof(TService), typeof(TImplementationService), typeof(TImplementation), implementationFactory, ServiceLifetime.Singleton, setupAction);
        }


        // TODO remove implementationServiceType after refactor

        /// <summary>
        /// Adds a service of given service and implementation type to the specified <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="serviceType">The type of the service to add</param>
        /// <param name="implementationServiceType">The type with which the implementation will be registered and resolved</param>
        /// <param name="implementationType">The type of the implementation to use</param>
        /// <param name="serviceLifetime">Service lifetime to use</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection Add(this IServiceCollection services, Type serviceType, Type implementationServiceType, Type implementationType, ServiceLifetime serviceLifetime, Action<DecoratorOptions> setupAction) {
            return AddInternal(services, serviceType, implementationType, null, serviceLifetime, setupAction);
        }

        /// <summary>
        /// Adds a service of given service and implementation type to the specified <see cref="IServiceCollection"/> using the provided factory
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to</param>
        /// <param name="serviceType">The type of the service to add</param>
        /// <param name="implementationServiceType">The type with which the implementation will be registered and resolved</param>
        /// <param name="implementationType">The type of the implementation to use</param>
        /// <param name="implementationFactory">The factory that creates the service</param>
        /// <param name="serviceLifetime">Service lifetime to use</param>
        /// <param name="setupAction">The action that sets up the decorators for this service</param>
        /// <returns>A reference to this instance after the operation has completed</returns>
        public static IServiceCollection Add(this IServiceCollection services, Type serviceType, Type implementationServiceType, Type implementationType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime serviceLifetime, Action<DecoratorOptions> setupAction) {
            return AddInternal(services, serviceType, implementationType, implementationFactory, serviceLifetime, setupAction);
        }
        
        private static IServiceCollection AddInternal(this IServiceCollection services, Type serviceType, Type implementationType, Func<IServiceProvider, object>? implementationFactory, ServiceLifetime serviceLifetime, Action<DecoratorOptions> setupAction) {
            services.AddProxy(serviceType, implementationType, serviceLifetime, setupAction);

            if (implementationFactory != null) {
                services.Add(new ServiceDescriptor(implementationType, implementationFactory, serviceLifetime));
            }
            else {
                services.Add(new ServiceDescriptor(implementationType, implementationType, serviceLifetime));
            }

            return services;
        }

        private static void AddProxy(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime serviceLifetime, Action<DecoratorOptions> setupAction) {
            var options = GetDecoratorOptions(serviceType, implementationType, setupAction);
            var proxyFactory = GetDecoratedProxyFactory(serviceType, implementationType, options);

            services.Add(new ServiceDescriptor(serviceType, proxyFactory, serviceLifetime));
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
                    throw new ServiceRegistrationException($"Service type '{serviceType.FullName}' has no accessible constructor; class service types require at least one accessible constructor.");
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
