using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Tests.Decorators.Targets {
    public abstract class ServiceCollectionTargetBase : IServiceCollectionTarget {
        public abstract string Value { get; set; }

        public abstract Task<string> GetValue();
    }
}   