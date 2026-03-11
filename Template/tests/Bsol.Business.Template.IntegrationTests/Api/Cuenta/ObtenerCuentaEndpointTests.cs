using System.Net;
using System.Net.Http.Json;
using Bsol.Business.Template.Api.Endpoints.Cuentas;
using Bsol.Business.Template.IntegrationTests.Utils;
using Shouldly;
using Xunit;

namespace Bsol.Business.Template.IntegrationTests.Api.Cuentas;

public class ObtenerCuentaEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ObtenerCuentaEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateApiClient();
    }

    [Fact]
    public async Task GetAccount_ShouldReturnOk_WhenAccountExists()
    {
        var accountId = "11111111-1111-1111-1111-111111111111";

        var response = await _client.GetAsync($"/accounts/{accountId}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<ObtenerCuentaResponse>();
        body.ShouldNotBeNull();
        body.Id.ShouldBe(Guid.Parse(accountId));
        body.NumeroCuenta.ShouldBe("1001");
        body.Saldo.ShouldBe(1000);
    }

    [Fact]
    public async Task GetAccount_ShouldReturnNotFound_WhenAccountDoesNotExist()
    {
        var response = await _client.GetAsync($"/accounts/{Guid.NewGuid()}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
}
