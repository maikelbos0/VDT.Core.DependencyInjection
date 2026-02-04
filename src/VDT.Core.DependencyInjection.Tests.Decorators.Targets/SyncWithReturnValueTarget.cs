using System;

namespace VDT.Core.DependencyInjection.Tests.Decorators.Targets;

public class SyncWithReturnValueTarget {
    public virtual bool Success() => true;

    public virtual bool Error() => throw new InvalidOperationException("Error class called");

    public virtual bool VerifyContext<TFoo>(TFoo foo, string bar) => true;
}
