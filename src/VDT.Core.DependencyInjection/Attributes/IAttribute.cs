using Microsoft.Extensions.DependencyInjection;

namespace VDT.Core.DependencyInjection.Attributes {
    internal interface IAttribute {
        ServiceLifetime ServiceLifetime { get; }
    }
}
