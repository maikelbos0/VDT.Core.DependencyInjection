using System.Linq;
using System.Reflection;
using VDT.Core.DependencyInjection.Decorators;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class DecoratorOptionsTests {
        private readonly DecoratorOptions options = new(typeof(IDecoratorOptionsTarget), typeof(DecoratorOptionsTarget));
        private readonly MethodInfo serviceDecoratedMethod = typeof(IDecoratorOptionsTarget).GetMethodStrict(nameof(IDecoratorOptionsTarget.ServiceDecorated));
        private readonly MethodInfo implementationDecoratedMethod = typeof(IDecoratorOptionsTarget).GetMethodStrict(nameof(IDecoratorOptionsTarget.ImplementationDecorated));
        private readonly MethodInfo undecoratedMethod = typeof(IDecoratorOptionsTarget).GetMethodStrict(nameof(IDecoratorOptionsTarget.Undecorated));

        [Fact]
        public void AddDecorator_Without_Predicate_Works() {
            options.AddDecorator<TestDecorator>();

            var policy = Assert.Single(options.Policies);
            Assert.True(policy.Predicate(serviceDecoratedMethod));
            Assert.True(policy.Predicate(implementationDecoratedMethod));
            Assert.True(policy.Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddDecorator_With_MethodInfo_Works() {
            options.AddDecorator<TestDecorator>(serviceDecoratedMethod);

            var policy = Assert.Single(options.Policies);
            Assert.True(policy.Predicate(serviceDecoratedMethod));
            Assert.False(policy.Predicate(implementationDecoratedMethod));
            Assert.False(policy.Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddDecorator_With_Predicate_Works() {
            options.AddDecorator<TestDecorator>(m => m == serviceDecoratedMethod);

            var policy = Assert.Single(options.Policies);
            Assert.True(policy.Predicate(serviceDecoratedMethod));
            Assert.False(policy.Predicate(implementationDecoratedMethod));
            Assert.False(policy.Predicate(undecoratedMethod));
        }

        [Fact]
        public void AddAttributeDecorators_Works() {
            options.AddAttributeDecorators();

            Assert.Single(options.Policies.Where(policy => policy.Predicate(serviceDecoratedMethod)));
            Assert.Single(options.Policies.Where(policy => policy.Predicate(implementationDecoratedMethod)));
            Assert.Empty(options.Policies.Where(policy => policy.Predicate(undecoratedMethod)));
        }
    }
}
