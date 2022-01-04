using Microsoft.AspNetCore.Http;

namespace SimpleFormsService.Test;

public class MockHttpContextAccessor : IHttpContextAccessor
{
    public HttpContext? HttpContext
    {
        get => new MockHttpContext();
        set { }
    }
}