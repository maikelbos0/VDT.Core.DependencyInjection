using Microsoft.Extensions.DependencyInjection;
using VDT.Core.DependencyInjection.Attributes;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Attributes {
    public class TransientAttributeTests {
        [Fact]
        public void TransientAttribute_ServiceLifetime_Is_Transient() {
            Assert.Equal(ServiceLifetime.Transient, new TransientAttribute().ServiceLifetime);
        }
    }
}
