using System.Net;
using System.Net.Http.Json;
using Bsol.Business.Template.Api.Endpoints.Transacciones;
using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.IntegrationTests.Utils;
using Shouldly;
using Xunit;

namespace Bsol.Business.Template.IntegrationTests.Api.Transacciones;

public class ObtenerTransaccionesEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ObtenerTransaccionesEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateApiClient();
    }

    [Fact]
    public async Task GetTransactions_ShouldReturnRegisteredTransactions()
    {
        var transferRequest = new TransferirRequest
        {
            SourceAccountNumber = "1001",
            DestinationAccountNumber = "1002",
            Amount = 50
        };

        var transferResponse = await _client.PostAsJsonAsync("/transactions", transferRequest);
        transferResponse.EnsureSuccessStatusCode();

        var response = await _client.GetAsync("/transactions");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<List<Transaccion>>();
        body.ShouldNotBeNull();
        body.Count.ShouldBeGreaterThan(0);
        body.ShouldContain(x => x.Monto == 50 && x.CodigoVoucher.Length == 8);
    }
}
