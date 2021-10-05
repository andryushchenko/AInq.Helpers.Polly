# AInq.Helpers.Polly

[![Nuget](https://img.shields.io/nuget/v/AInq.Helpers.Polly)](https://www.nuget.org/packages/AInq.Helpers.Polly/) [![GitHub release (latest by date)](https://img.shields.io/github/v/release/andryushchenko/AInq.Helpers.Polly)](https://github.com/andryushchenko/AInq.Helpers.Polly/releases) [![netstandard 2.0](https://img.shields.io/badge/netstandard-2.0-blue.svg)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) [![netstandard 2.1](https://img.shields.io/badge/netstandard-2.1-blue.svg)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard) [![net 5.0](https://img.shields.io/badge/net-5.0-blue.svg)](https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet) [![net 6.0](https://img.shields.io/badge/net-6.0-blue.svg)](https://dotnet.microsoft.com/learn/dotnet/what-is-dotnet) [![GitHub](https://img.shields.io/github/license/andryushchenko/AInq.Helpers.Polly)](LICENSE)

## ANNOUNCEMENT

**This repo is now archived**

All work moved to [AInq.Helpers](https://github.com/andryushchenko/AInq.Helpers) 

---

## What is it?

Simple helpers library to use with [Polly](https://github.com/App-vNext/Polly)

- Ready to use retry policies with logging
  - `TransientRetryAsyncPolicy` for transient errors
  - `TimeoutRetryAsyncPolicy` for HTTP 429
- Helpers to store data in `Polly.Context`
- Helpers to execute HTTP requests with `Polly.IAsyncPolicy<HttpResponseMessage>`

## Documentation

As for now documentation is provided in this document and by XML documentation inside packages.

## Contribution

If you find a bug, have a question or something else - you are friendly welcome to open an issue.

## License
Copyright © 2021 Anton Andryushchenko. AInq.Helpers.Polly is licensed under [Apache License 2.0](LICENSE)
