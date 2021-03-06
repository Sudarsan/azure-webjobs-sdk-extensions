﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Timers;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace Microsoft.Azure.WebJobs.Extensions.Tests.Common
{
    public class TestExceptionHandlerFactory : IWebJobsExceptionHandlerFactory
    {
        private TestExceptionHandler _handler = new TestExceptionHandler();

        public IWebJobsExceptionHandler Create(IHost jobHost) => _handler;

        private class TestExceptionHandler : IWebJobsExceptionHandler
        {
            public void Initialize(JobHost host)
            {
            }

            public Task OnTimeoutExceptionAsync(ExceptionDispatchInfo exceptionInfo, TimeSpan timeoutGracePeriod)
            {
                Assert.True(false, $"Timeout exception in test exception handler: {exceptionInfo.SourceException}");
                return Task.CompletedTask;
            }

            public Task OnUnhandledExceptionAsync(ExceptionDispatchInfo exceptionInfo)
            {
                Assert.True(false, $"Error in test exception handler: {exceptionInfo.SourceException}");
                return Task.CompletedTask;
            }
        }
    }
}
