// Copyright 2021 Anton Andryushchenko
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
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AInq.Helpers.Polly
{

public static class HttpPolicyHelper
{
    private static async Task<HttpResponseMessage> RequestAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        HttpMethod method, HttpContent? content, ILogger logger, CancellationToken cancellation = default)
    {
        using var request = new HttpRequestMessage(method, url) {Content = content};
        var result = await policy
                           .ExecuteAsync((ctx, cancel)
                                   => ctx.Get<HttpClient>("client")!.SendAsync(ctx.Get<HttpRequestMessage>("request")!, cancel),
                               new Context().With("client", client).With("request", request).WithCancellation(cancellation).WithLogger(logger),
                               cancellation,
                               false)
                           .ConfigureAwait(false);
        logger.LogDebug("HTTP {Method} to {Url} - {Code}",
            result.RequestMessage?.Method,
            result.RequestMessage?.RequestUri,
            result.StatusCode);
        return result;
    }

#region Get

    public static async Task<HttpResponseMessage> GetAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        ILogger logger, CancellationToken cancellation = default)
        => await (policy ?? throw new ArgumentNullException(nameof(policy)))
                 .RequestAsync(client ?? throw new ArgumentNullException(nameof(client)),
                     url,
                     HttpMethod.Get,
                     null,
                     logger ?? throw new ArgumentNullException(nameof(logger)),
                     cancellation)
                 .ConfigureAwait(false);

    public static async Task<HttpResponseMessage> GetAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        CancellationToken cancellation = default)
        => await (policy ?? throw new ArgumentNullException(nameof(policy)))
                 .RequestAsync(client ?? throw new ArgumentNullException(nameof(client)),
                     url,
                     HttpMethod.Get,
                     null,
                     NullLogger.Instance,
                     cancellation)
                 .ConfigureAwait(false);

    public static async Task<HttpResponseMessage> GetAsync(this IAsyncPolicy<HttpResponseMessage> policy, string url, ILogger logger,
        CancellationToken cancellation = default)
    {
        using var client = new HttpClient();
        return await (policy ?? throw new ArgumentNullException(nameof(policy)))
                     .RequestAsync(client,
                         string.IsNullOrWhiteSpace(url) ? throw new ArgumentNullException(nameof(url)) : url,
                         HttpMethod.Get,
                         null,
                         logger ?? throw new ArgumentNullException(nameof(logger)),
                         cancellation)
                     .ConfigureAwait(false);
    }

    public static async Task<HttpResponseMessage> GetAsync(this IAsyncPolicy<HttpResponseMessage> policy, string url,
        CancellationToken cancellation = default)
    {
        using var client = new HttpClient();
        return await (policy ?? throw new ArgumentNullException(nameof(policy)))
                     .RequestAsync(client,
                         string.IsNullOrWhiteSpace(url) ? throw new ArgumentNullException(nameof(url)) : url,
                         HttpMethod.Get,
                         null,
                         NullLogger.Instance,
                         cancellation)
                     .ConfigureAwait(false);
    }

#endregion

#region Delete

    public static async Task<HttpResponseMessage> DeleteAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        ILogger logger, CancellationToken cancellation = default)
        => await (policy ?? throw new ArgumentNullException(nameof(policy)))
                 .RequestAsync(client ?? throw new ArgumentNullException(nameof(client)),
                     url,
                     HttpMethod.Delete,
                     null,
                     logger ?? throw new ArgumentNullException(nameof(logger)),
                     cancellation)
                 .ConfigureAwait(false);

    public static async Task<HttpResponseMessage> DeleteAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        CancellationToken cancellation = default)
        => await (policy ?? throw new ArgumentNullException(nameof(policy)))
                 .RequestAsync(client ?? throw new ArgumentNullException(nameof(client)),
                     url,
                     HttpMethod.Delete,
                     null,
                     NullLogger.Instance,
                     cancellation)
                 .ConfigureAwait(false);

    public static async Task<HttpResponseMessage> DeleteAsync(this IAsyncPolicy<HttpResponseMessage> policy, string url, ILogger logger,
        CancellationToken cancellation = default)
    {
        using var client = new HttpClient();
        return await (policy ?? throw new ArgumentNullException(nameof(policy)))
                     .RequestAsync(client,
                         string.IsNullOrWhiteSpace(url) ? throw new ArgumentNullException(nameof(url)) : url,
                         HttpMethod.Delete,
                         null,
                         logger ?? throw new ArgumentNullException(nameof(logger)),
                         cancellation)
                     .ConfigureAwait(false);
    }

    public static async Task<HttpResponseMessage> DeleteAsync(this IAsyncPolicy<HttpResponseMessage> policy, string url,
        CancellationToken cancellation = default)
    {
        using var client = new HttpClient();
        return await (policy ?? throw new ArgumentNullException(nameof(policy)))
                     .RequestAsync(client,
                         string.IsNullOrWhiteSpace(url) ? throw new ArgumentNullException(nameof(url)) : url,
                         HttpMethod.Delete,
                         null,
                         NullLogger.Instance,
                         cancellation)
                     .ConfigureAwait(false);
    }

#endregion

#region Post

    public static async Task<HttpResponseMessage> PostAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        HttpContent content, ILogger logger, CancellationToken cancellation = default)
        => await (policy ?? throw new ArgumentNullException(nameof(policy)))
                 .RequestAsync(client ?? throw new ArgumentNullException(nameof(client)),
                     url,
                     HttpMethod.Post,
                     content ?? throw new ArgumentNullException(nameof(content)),
                     logger ?? throw new ArgumentNullException(nameof(logger)),
                     cancellation)
                 .ConfigureAwait(false);

    public static async Task<HttpResponseMessage> PostAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        HttpContent content, CancellationToken cancellation = default)
        => await (policy ?? throw new ArgumentNullException(nameof(policy)))
                 .RequestAsync(client ?? throw new ArgumentNullException(nameof(client)),
                     url,
                     HttpMethod.Post,
                     content ?? throw new ArgumentNullException(nameof(content)),
                     NullLogger.Instance,
                     cancellation)
                 .ConfigureAwait(false);

    public static async Task<HttpResponseMessage> PostAsync(this IAsyncPolicy<HttpResponseMessage> policy, string url, HttpContent content,
        ILogger logger, CancellationToken cancellation = default)
    {
        using var client = new HttpClient();
        return await (policy ?? throw new ArgumentNullException(nameof(policy)))
                     .RequestAsync(client,
                         string.IsNullOrWhiteSpace(url) ? throw new ArgumentNullException(nameof(url)) : url,
                         HttpMethod.Post,
                         content ?? throw new ArgumentNullException(nameof(content)),
                         logger ?? throw new ArgumentNullException(nameof(logger)),
                         cancellation)
                     .ConfigureAwait(false);
    }

    public static async Task<HttpResponseMessage> PostAsync(this IAsyncPolicy<HttpResponseMessage> policy, string url, HttpContent content,
        CancellationToken cancellation = default)
    {
        using var client = new HttpClient();
        return await (policy ?? throw new ArgumentNullException(nameof(policy)))
                     .RequestAsync(client,
                         string.IsNullOrWhiteSpace(url) ? throw new ArgumentNullException(nameof(url)) : url,
                         HttpMethod.Post,
                         content ?? throw new ArgumentNullException(nameof(content)),
                         NullLogger.Instance,
                         cancellation)
                     .ConfigureAwait(false);
    }

#endregion

#region Put

    public static async Task<HttpResponseMessage> PutAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        HttpContent content, ILogger logger, CancellationToken cancellation = default)
        => await (policy ?? throw new ArgumentNullException(nameof(policy)))
                 .RequestAsync(client ?? throw new ArgumentNullException(nameof(client)),
                     url,
                     HttpMethod.Put,
                     content ?? throw new ArgumentNullException(nameof(content)),
                     logger ?? throw new ArgumentNullException(nameof(logger)),
                     cancellation)
                 .ConfigureAwait(false);

    public static async Task<HttpResponseMessage> PutAsync(this IAsyncPolicy<HttpResponseMessage> policy, HttpClient client, string url,
        HttpContent content, CancellationToken cancellation = default)
        => await (policy ?? throw new ArgumentNullException(nameof(policy)))
                 .RequestAsync(client ?? throw new ArgumentNullException(nameof(client)),
                     url,
                     HttpMethod.Put,
                     content ?? throw new ArgumentNullException(nameof(content)),
                     NullLogger.Instance,
                     cancellation)
                 .ConfigureAwait(false);

    public static async Task<HttpResponseMessage> PutAsync(this IAsyncPolicy<HttpResponseMessage> policy, string url, HttpContent content,
        ILogger logger, CancellationToken cancellation = default)
    {
        using var client = new HttpClient();
        return await (policy ?? throw new ArgumentNullException(nameof(policy)))
                     .RequestAsync(client,
                         string.IsNullOrWhiteSpace(url) ? throw new ArgumentNullException(nameof(url)) : url,
                         HttpMethod.Put,
                         content ?? throw new ArgumentNullException(nameof(content)),
                         logger ?? throw new ArgumentNullException(nameof(logger)),
                         cancellation)
                     .ConfigureAwait(false);
    }

    public static async Task<HttpResponseMessage> PutAsync(this IAsyncPolicy<HttpResponseMessage> policy, string url, HttpContent content,
        CancellationToken cancellation = default)
    {
        using var client = new HttpClient();
        return await (policy ?? throw new ArgumentNullException(nameof(policy)))
                     .RequestAsync(client,
                         string.IsNullOrWhiteSpace(url) ? throw new ArgumentNullException(nameof(url)) : url,
                         HttpMethod.Put,
                         content ?? throw new ArgumentNullException(nameof(content)),
                         NullLogger.Instance,
                         cancellation)
                     .ConfigureAwait(false);
    }

#endregion
}

}
