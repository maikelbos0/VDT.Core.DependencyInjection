using Microsoft.Extensions.DependencyInjection;

namespace VDT.Core.DependencyInjection.Attributes {
    internal interface IImplementationOnlyAttribute {
        ServiceLifetime ServiceLifetime { get; }
    }
}
