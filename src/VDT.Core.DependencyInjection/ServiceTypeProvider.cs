﻿using System;
using System.Collections.Generic;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Signature for methods that return service types for a given implementation type
    /// </summary>
    /// <param name="implementationType">The implementation type for which to return service types</param>
    /// <returns>An enumerable of service types to register this implementation type for</returns>
    /// <remarks>When using decorators, the service types must differ from the implementation type</remarks>
    [Obsolete($"The separate delegates {nameof(ServiceTypeProvider)} and {nameof(ServiceLifetimeProvider)} have been deprecated; they have been replaced by the delegate {nameof(ServiceRegistrationProvider)} that returns both the service type and lifetime. This delegate will be removed in a future version.")]
    public delegate IEnumerable<Type> ServiceTypeProvider(Type implementationType);
}
