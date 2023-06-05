using System;
using System.Reflection;

namespace VDT.Core.DependencyInjection.Decorators {
    internal sealed class DecoratorPolicy {
        internal Type DecoratorType { get; }
        internal Predicate<MethodInfo> Predicate { get; }

        internal DecoratorPolicy(Type decoratorType, Predicate<MethodInfo> predicate) {
            DecoratorType = decoratorType;
            Predicate = predicate;
        }
    }
}
