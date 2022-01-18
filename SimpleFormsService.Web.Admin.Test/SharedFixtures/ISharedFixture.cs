using System;

namespace SimpleFormsService.Test.SharedFixtures
{
    public interface ISharedFixture
    {
        IServiceProvider Container { get; set; }
    }
}