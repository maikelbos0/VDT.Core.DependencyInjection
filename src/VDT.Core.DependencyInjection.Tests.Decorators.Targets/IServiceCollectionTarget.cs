﻿using System.Threading.Tasks;

namespace VDT.Core.DependencyInjection.Tests.Decorators.Targets {
    public interface IServiceCollectionTarget {
        string Value { get; set; }

        Task<string> GetValue();
    }
}