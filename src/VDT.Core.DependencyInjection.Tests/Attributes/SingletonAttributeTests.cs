using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class SingletonAttributeTests {
        [Fact]
        public void SingletonAttribute_ServiceLifetime_Is_Singleton() {
            Assert.Equal(ServiceLifetime.Singleton, new SingletonAttribute().ServiceLifetime);
        }
    }
}
