﻿// Copyright 2021 Anton Andryushchenko
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Polly;
using System;
using System.Threading;

namespace AInq.Helpers.Polly
{

public static class ContextHelper
{
    private const string CancellationKey = "cancellation";
    private const string LoggerKey = "logger";

    public static T? Get<T>(this Context context, string key)
        => (context ?? throw new ArgumentNullException(nameof(context)))
           .TryGetValue(string.IsNullOrWhiteSpace(key) ? throw new ArgumentNullException(nameof(key)) : key, out var value)
           && value is T result
            ? result
            : default;

    public static Context With(this Context context, string key, object value)
    {
        (context ?? throw new ArgumentNullException(nameof(context)))[string.IsNullOrWhiteSpace(key)
            ? throw new ArgumentNullException(nameof(key))
            : key] = value;
        return context;
    }

    public static Context WithCancellation(this Context context, CancellationToken cancellation)
        => context.With(CancellationKey, cancellation);

    public static CancellationToken GetCancellationToken(this Context context, string key = CancellationKey)
        => context.Get<CancellationToken>(key);

    public static Context WithLogger(this Context context, ILogger logger)
        => context.With(LoggerKey, logger ?? throw new ArgumentNullException(nameof(logger)));

    public static ILogger GetLogger(this Context context, string key = LoggerKey)
        => context.Get<ILogger>(key) ?? NullLogger.Instance;
}

}
