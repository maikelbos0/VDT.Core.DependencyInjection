using System.Reflection;
using System;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public static class TypeExtensions {
        public static MethodInfo GetMethodStrict(this Type type, string name) {
            return type.GetMethod(name) ?? throw new InvalidOperationException($"Method '{type.FullName}.{name}' was not found.");
        }
    }
}
