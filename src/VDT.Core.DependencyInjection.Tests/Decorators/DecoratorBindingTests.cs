using VDT.Core.DependencyInjection.Decorators;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class DecoratorBindingTests {
        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Self() {
            var binding = new DecoratorBinding(
                typeof(IDecoratorBindingTarget).GetMethodStrict(nameof(IDecoratorBindingTarget.Method)),
                typeof(IDecoratorBindingTarget),
                null!
            );

            Assert.Equal(binding.GetServiceMethod(), typeof(IDecoratorBindingTarget).GetMethodStrict(nameof(IDecoratorBindingTarget.Method)));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Interface() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethodStrict(nameof(DecoratorBindingTarget.Method)),
                typeof(IDecoratorBindingTarget),
                null!
            );

            Assert.Equal(binding.GetServiceMethod(), typeof(IDecoratorBindingTarget).GetMethodStrict(nameof(IDecoratorBindingTarget.Method)));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Null_When_Missing_On_Interface() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethodStrict(nameof(DecoratorBindingTarget.ImplementationMethod)),
                typeof(IDecoratorBindingTarget),
                null!
            );

            Assert.Null(binding.GetServiceMethod());
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Abstract_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethodStrict(nameof(DecoratorBindingTarget.Method)),
                typeof(DecoratorBindingTargetAbstractBase),
                null!
            );

            Assert.Equal(binding.GetServiceMethod(), typeof(DecoratorBindingTargetAbstractBase).GetMethodStrict(nameof(DecoratorBindingTargetAbstractBase.Method)));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Null_When_Missing_On_Abstract_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethodStrict(nameof(DecoratorBindingTarget.ImplementationMethod)),
                typeof(DecoratorBindingTargetAbstractBase),
                null!
            );

            Assert.Null(binding.GetServiceMethod());
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethodStrict(nameof(DecoratorBindingTarget.Method)),
                typeof(DecoratorBindingTargetBase),
                null!
            );

            Assert.Equal(binding.GetServiceMethod(), typeof(DecoratorBindingTargetBase).GetMethodStrict(nameof(DecoratorBindingTargetBase.Method)));
        }

        [Fact]
        public void ServiceMethod_Resolves_Correctly_To_Null_When_Missing_On_Base_Class() {
            var binding = new DecoratorBinding(
                typeof(DecoratorBindingTarget).GetMethodStrict(nameof(DecoratorBindingTarget.ImplementationMethod)),
                typeof(DecoratorBindingTargetBase),
                null!
            );

            Assert.Null(binding.GetServiceMethod());
        }
    }
}
