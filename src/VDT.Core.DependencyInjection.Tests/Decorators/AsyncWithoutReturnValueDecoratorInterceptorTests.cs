﻿using System;
using System.Threading.Tasks;
using VDT.Core.DependencyInjection.Tests.Decorators.Targets;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public sealed class AsyncWithoutReturnValueDecoratorInterceptorTests : DecoratorInterceptorTests<AsyncWithoutReturnValueTarget> {
        public override async Task Success(AsyncWithoutReturnValueTarget target) {
            await target.Success();
        }

        public override async Task Error(AsyncWithoutReturnValueTarget target) {
            await Assert.ThrowsAsync<InvalidOperationException>(() => target.Error());
        }

        public override async Task VerifyContext(AsyncWithoutReturnValueTarget target) {
            await target.VerifyContext(42, "Foo");
        }
    }
}
