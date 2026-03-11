using System.Net;
using System.Net.Http.Json;
using Bsol.Business.Template.Api.Endpoints.Transacciones;
using Bsol.Business.Template.IntegrationTests.Utils;
using Shouldly;
using Xunit;

namespace Bsol.Business.Template.IntegrationTests.Api.Transacciones;

public class TransferirEndpointTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TransferirEndpointTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateApiClient();
    }

    [Fact]
    public async Task PostTransaction_ShouldReturnOk_AndVoucherData()
    {
        var request = new TransferirRequest
        {
            SourceAccountNumber = "1001",
            DestinationAccountNumber = "1002",
            Amount = 100
        };

        var response = await _client.PostAsJsonAsync("/transactions", request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var body = await response.Content.ReadFromJsonAsync<TransferirResponse>();
        body.ShouldNotBeNull();
        body.TransactionId.ShouldNotBe(Guid.Empty);
        body.VoucherCode.ShouldNotBeNullOrWhiteSpace();
    }
}
