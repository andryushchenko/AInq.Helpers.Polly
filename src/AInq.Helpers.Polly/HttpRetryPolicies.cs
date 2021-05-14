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
using Polly;
using System;
using System.Net;
using System.Net.Http;

namespace AInq.Helpers.Polly
{

public static class HttpRetryPolicies
{
    public static IAsyncPolicy<HttpResponseMessage> TransientRetryAsyncPolicy()
        => Policy.Handle<HttpRequestException>()
                 .OrResult<HttpResponseMessage>(response
                     => response.StatusCode == HttpStatusCode.RequestTimeout || (int) response.StatusCode >= 500)
                 .RetryForeverAsync(OnTransientRetry);

    public static IAsyncPolicy<HttpResponseMessage> TimeoutRetryAsyncPolicy(TimeSpan timeout)
        => Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == (HttpStatusCode) 429)
                 .WaitAndRetryForeverAsync((attempt, _) => TimeSpan.FromTicks(timeout.Ticks * attempt * attempt), OnTimeoutRetry);

    private static void OnTimeoutRetry(DelegateResult<HttpResponseMessage> result, int attempt, TimeSpan wait, Context context)
        => context.GetLogger()
                  .LogWarning("HTTP {Method} to {Url} failed with {Code} - retry {Attempt} after {Timeout}",
                      result.Result.RequestMessage?.Method,
                      result.Result.RequestMessage?.RequestUri,
                      result.Result.StatusCode,
                      attempt,
                      wait);

    private static void OnTransientRetry(DelegateResult<HttpResponseMessage> result, int attempt, Context context)
    {
        if (result.Exception == null)
            context.GetLogger()
                   .LogWarning("HTTP {Method} to {Url} failed with {Code} - retry {Attempt}",
                       result.Result.RequestMessage?.Method,
                       result.Result.RequestMessage?.RequestUri,
                       result.Result.StatusCode,
                       attempt);
        else
            context.GetLogger().LogWarning(result.Exception, "HTTP Request failed with exception - retry {Attempt}", attempt);
    }
}

}
