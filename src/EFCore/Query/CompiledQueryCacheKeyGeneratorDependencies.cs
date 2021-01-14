// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

#nullable enable

namespace Microsoft.EntityFrameworkCore.Query
{
    /// <summary>
    ///     <para>
    ///         Service dependencies parameter class for <see cref="CompiledQueryCacheKeyGenerator" />
    ///     </para>
    ///     <para>
    ///         This type is typically used by database providers (and other extensions). It is generally
    ///         not used in application code.
    ///     </para>
    ///     <para>
    ///         Do not construct instances of this class directly from either provider or application code as the
    ///         constructor signature may change as new dependencies are added. Instead, use this type in
    ///         your constructor so that an instance will be created and injected automatically by the
    ///         dependency injection container. To create an instance with some dependent services replaced,
    ///         first resolve the object from the dependency injection container, then replace selected
    ///         services using the 'With...' methods. Do not call the constructor at any point in this process.
    ///     </para>
    ///     <para>
    ///         The service lifetime is <see cref="ServiceLifetime.Scoped" />. This means that each
    ///         <see cref="DbContext" /> instance will use its own instance of this service.
    ///         The implementation may depend on other services registered with any lifetime.
    ///         The implementation does not need to be thread-safe.
    ///     </para>
    /// </summary>
    public sealed record CompiledQueryCacheKeyGeneratorDependencies
    {
        /// <summary>
        ///     <para>
        ///         Creates the service dependencies parameter object for a <see cref="CompiledQueryCacheKeyGenerator" />.
        ///     </para>
        ///     <para>
        ///         This type is typically used by database providers (and other extensions). It is generally
        ///         not used in application code.
        ///     </para>
        ///     <para>
        ///         Do not call this constructor directly from either provider or application code as it may change
        ///         as new dependencies are added. Instead, use this type in your constructor so that an instance
        ///         will be created and injected automatically by the dependency injection container. To create
        ///         an instance with some dependent services replaced, first resolve the object from the dependency
        ///         injection container, then replace selected services using the 'With...' methods. Do not call
        ///         the constructor at any point in this process.
        ///     </para>
        ///     <para>
        ///         The service lifetime is <see cref="ServiceLifetime.Scoped" />. This means that each
        ///         <see cref="DbContext" /> instance will use its own instance of this service.
        ///         The implementation may depend on other services registered with any lifetime.
        ///         The implementation does not need to be thread-safe.
        ///     </para>
        /// </summary>
        [EntityFrameworkInternal]
        public CompiledQueryCacheKeyGeneratorDependencies(
            [NotNull] IModel model,
            [NotNull] ICurrentDbContext currentContext,
            [NotNull] IExecutionStrategyFactory executionStrategyFactory)
        {
            Check.NotNull(model, nameof(model));
            Check.NotNull(currentContext, nameof(currentContext));
            Check.NotNull(executionStrategyFactory, nameof(executionStrategyFactory));

            Model = model;
            CurrentContext = currentContext;
            IsRetryingExecutionStrategy = executionStrategyFactory.Create().RetriesOnFailure;
        }

        /// <summary>
        ///     The model that queries will be written against.
        /// </summary>
        public IModel Model { get; [param: NotNull] init; }

        /// <summary>
        ///     The context that queries will be executed for.
        /// </summary>
        public ICurrentDbContext CurrentContext { get; [param: NotNull] init; }

        /// <summary>
        ///     Whether the configured execution strategy can retry.
        /// </summary>
        public bool IsRetryingExecutionStrategy { get; init; }
    }
}
