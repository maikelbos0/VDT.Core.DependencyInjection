using System;
using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Tests.Decorators.Targets;

public class AsyncWithoutReturnValueTarget {
    public virtual async Task Success() => await Task.Yield();

    public virtual async Task Error() {
        await Task.Yield();

        throw new InvalidOperationException("Error class called");
    }

    public virtual async Task VerifyContext<TFoo>(TFoo foo, string bar) => await Task.Yield();
}
