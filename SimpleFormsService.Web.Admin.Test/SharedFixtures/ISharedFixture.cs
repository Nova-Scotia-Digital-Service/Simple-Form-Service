using System;

namespace SimpleFormsService.Web.Admin.Test.SharedFixtures
{
    public interface ISharedFixture
    {
        IServiceProvider Container { get; set; }
    }
}