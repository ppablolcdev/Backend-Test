using Bsol.Business.Template.IntegrationTests.Mock.Service;
using Bsol.Business.Template.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bsol.Business.Template.IntegrationTests.Utils;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly WireMockSetup _wireMockSetup;

    public CustomWebApplicationFactory()
    {
        _wireMockSetup = new WireMockSetup();
        _wireMockSetup.AddConfiguration(PokeApiMockService.Configure);
        _wireMockSetup.ApplyConfigurations();
    }

    public HttpClient CreateApiClient()
    {
        var databaseName = $"BancoDbTests-{Guid.NewGuid()}";

        return WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Test");
            builder.UseSetting("PokeApiService:BaseUrl", _wireMockSetup.BaseUrl);
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<AppDbContext>));
                services.RemoveAll(typeof(AppDbContext));
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName));
            });
        }).CreateClient();
    }

    public new void Dispose()
    {
        _wireMockSetup.Dispose();
        base.Dispose();
    }
}
