using Microsoft.AspNetCore.Http;

namespace SimpleFormsService.Web.Admin.Test;

public class MockHttpContextAccessor : IHttpContextAccessor
{
    public HttpContext? HttpContext
    {
        get => new MockHttpContext();
        set { }
    }
}