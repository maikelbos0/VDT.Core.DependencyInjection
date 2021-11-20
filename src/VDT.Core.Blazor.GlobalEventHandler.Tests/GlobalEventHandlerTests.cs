﻿using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace VDT.Core.Blazor.GlobalEventHandler.Tests {
    public class GlobalEventHandlerTests {
        private class TestGlobalEventHandler : GlobalEventHandler {
            public new bool ShouldRender() => base.ShouldRender();
            public new async Task OnAfterRenderAsync(bool firstRender) => await base.OnAfterRenderAsync(firstRender);
        }

        [Fact]
        public void GlobalEventHandler_ShouldRender_Returns_False() {
            var subject = new TestGlobalEventHandler();

            Assert.False(subject.ShouldRender());
        }

        [Fact]
        public async Task GlobalEventHandler_OnAfterRenderAsync_Calls_JS_Module_Register_On_First_Render() {
            var runtime = Substitute.For<IJSRuntime>();
            var module = Substitute.For<IJSObjectReference>();
            runtime.InvokeAsync<IJSObjectReference>("import", new object[] { GlobalEventHandler.ModuleLocation }).ReturnsForAnyArgs(module);

            var subject = new TestGlobalEventHandler();
            subject.JSRuntime = runtime;
            await subject.OnAfterRenderAsync(true);

            await module.Received().InvokeVoidAsync("register", Arg.Is<object[]>(args => Assert.Single(args) is DotNetObjectReference<GlobalEventHandler>));
        }

        [Fact]
        public async Task GlobalEventHandler_OnAfterRenderAsync_Does_Not_Call_JS_Module_Register_After_First_Render() {
            var runtime = Substitute.For<IJSRuntime>();
            var module = Substitute.For<IJSObjectReference>();
            runtime.InvokeAsync<IJSObjectReference>("import", new object[] { GlobalEventHandler.ModuleLocation }).ReturnsForAnyArgs(module);

            var subject = new TestGlobalEventHandler();
            subject.JSRuntime = runtime;
            await subject.OnAfterRenderAsync(false);

            await module.DidNotReceiveWithAnyArgs().InvokeVoidAsync(Arg.Any<string>(), Arg.Any<object[]>());
        }

        [Fact]
        public async Task GlobalEventHandler_DisposeAsync_Calls_JS_Module_Unregister() {
            var runtime = Substitute.For<IJSRuntime>();
            var module = Substitute.For<IJSObjectReference>();
            runtime.InvokeAsync<IJSObjectReference>("import", new object[] { GlobalEventHandler.ModuleLocation }).ReturnsForAnyArgs(module);

            await using (var subject = new TestGlobalEventHandler()) {
                subject.JSRuntime = runtime;
                await subject.OnAfterRenderAsync(true);
            }

            await module.Received().InvokeVoidAsync("unregister", Arg.Is<object[]>(args => Assert.Single(args) is DotNetObjectReference<GlobalEventHandler>));
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeKeyDown_Invokes_OnKeyDown_Handler() {
            KeyboardEventArgs expected = new KeyboardEventArgs();
            KeyboardEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnKeyDown, (args) => actual = args));

            await handler.Instance.InvokeKeyDown(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeKeyUp_Invokes_OnKeyUp_Handler() {
            KeyboardEventArgs expected = new KeyboardEventArgs();
            KeyboardEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnKeyUp, (args) => actual = args));

            await handler.Instance.InvokeKeyUp(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeResize_Invokes_OnResize_Handler() {
            ResizeEventArgs expected = new ResizeEventArgs(0, 0);
            ResizeEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnResize, (args) => actual = args));

            await handler.Instance.InvokeResize(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeClick_Invokes_OnClick_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnClick, (args) => actual = args));

            await handler.Instance.InvokeClick(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseDown_Invokes_OnMouseDown_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnMouseDown, (args) => actual = args));

            await handler.Instance.InvokeMouseDown(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseUp_Invokes_OnMouseUp_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnMouseUp, (args) => actual = args));

            await handler.Instance.InvokeMouseUp(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeMouseMove_Invokes_OnMouseMove_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnMouseMove, (args) => actual = args));

            await handler.Instance.InvokeMouseMove(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeContextMenu_Invokes_OnContextMenu_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnContextMenu, (args) => actual = args));

            await handler.Instance.InvokeContextMenu(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GlobalEventHandler_InvokeDoubleClick_Invokes_OnDoubleClick_Handler() {
            MouseEventArgs expected = new MouseEventArgs();
            MouseEventArgs actual = null!;

            using var context = GetTestContext();
            var handler = context.RenderComponent<GlobalEventHandler>(parameters => parameters.Add(p => p.OnContextMenu, (args) => actual = args));

            await handler.Instance.InvokeContextMenu(expected);

            Assert.Equal(expected, actual);
        }

        private TestContext GetTestContext() {
            var context = new TestContext();

            context.JSInterop.SetupModule(GlobalEventHandler.ModuleLocation).SetupVoid("register", _ => true);

            return context;
        }
    }
}
