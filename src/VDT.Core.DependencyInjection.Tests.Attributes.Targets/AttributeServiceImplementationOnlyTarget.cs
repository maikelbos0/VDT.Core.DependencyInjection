﻿using VDT.Core.DependencyInjection.Attributes;

namespace VDT.Core.DependencyInjection.Tests.Attributes.Targets {
    [SingletonService(typeof(AttributeServiceImplementationOnlyTarget))]
    public class AttributeServiceImplementationOnlyTarget { }
}
