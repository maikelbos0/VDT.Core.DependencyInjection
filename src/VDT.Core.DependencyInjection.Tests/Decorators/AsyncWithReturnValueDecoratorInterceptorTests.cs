﻿using System;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.DependencyInjection.Tests.Decorators {
    public class AsyncWithReturnValueDecoratorInterceptorTests : DecoratorInterceptorTests<AsyncWithReturnValueTarget> {
        public override async Task Success(AsyncWithReturnValueTarget target) {
            Assert.True(await target.Success());
        }

        public override async Task Error(AsyncWithReturnValueTarget target) {
            await Assert.ThrowsAsync<InvalidOperationException>(() => target.Error());
        }

        public override async Task VerifyContext(AsyncWithReturnValueTarget target) {
            Assert.True(await target.VerifyContext(42, "Foo"));
        }
    }
}
