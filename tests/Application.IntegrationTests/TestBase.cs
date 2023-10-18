using NUnit.Framework;

using static Sirus.Application.IntegrationTests.Testing;

namespace Sirus.Application.IntegrationTests;
public class TestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
