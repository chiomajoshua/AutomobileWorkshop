﻿using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Infrastructure.Helpers;

public static class HttpContextHelper
{
    private static IHttpContextAccessor? _httpContextAccessor;

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public static HttpContext Current => _httpContextAccessor?.HttpContext ?? throw new InvalidOperationException("HttpContextAccessor has not been configured.");
}