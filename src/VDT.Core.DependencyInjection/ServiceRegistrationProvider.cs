using System;
using System.Collections.Generic;

namespace VDT.Core.DependencyInjection {
    /// <summary>
    /// Signature for methods that return service types for a given implementation type
    /// </summary>
    /// <param name="implementationType">The implementation type for which to return service types</param>
    /// <returns>An enumerable of registrations for the implementation type</returns>
    public delegate IEnumerable<ServiceRegistration> ServiceRegistrationProvider(Type implementationType);
}
