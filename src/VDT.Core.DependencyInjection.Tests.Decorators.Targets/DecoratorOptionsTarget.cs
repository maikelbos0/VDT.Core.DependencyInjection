namespace VDT.Core.DependencyInjection.Tests.Decorators.Targets {
    public class DecoratorOptionsTarget : IDecoratorOptionsTarget {
        public void ServiceDecorated() {
        }

        [TestDecorator]
        public void ImplementationDecorated() {
        }

        public void Undecorated() {
        }
    }
}
