using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class ScopedAttributeTests {
        [Fact]
        public void ScopedAttribute_ServiceLifetime_Is_Scoped() {
            Assert.Equal(ServiceLifetime.Scoped, new ScopedAttribute().ServiceLifetime);
        }
    }
}
