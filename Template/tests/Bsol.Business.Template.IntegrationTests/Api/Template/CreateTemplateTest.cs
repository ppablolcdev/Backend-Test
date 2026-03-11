using System.Net;
using System.Text;
using System.Text.Json;
//using Bsol.Business.Template.Api.Endpoints.Template;
using Bsol.Business.Template.IntegrationTests.Builder;
using Bsol.Business.Template.IntegrationTests.Data;
using Bsol.Business.Template.IntegrationTests.Utils;
using Bsol.Business.Template.SharedKernel.Interfaces;
using Shouldly;
using Xunit;

namespace Bsol.Business.Template.IntegrationTests.Api.Template;

public class CreateTemplateTest : BaseEfRepositoryTest, IClassFixture<CustomWebApplicationFactory>
{
    //private readonly string _url = CreateTemplateRequest.Route;
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;

    public CreateTemplateTest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    //public static IEnumerable<object[]> GetTemplateRequests()
    //{
    //    yield return new object[]
    //    {
    //        new CreateTemplateBuilder("Template 1").Build(),
    //        string.Empty,
    //        HttpStatusCode.OK
    //    };
    //    yield return new object[]
    //    {
    //        new CreateTemplateBuilder("").Build(),
    //        "",
    //        HttpStatusCode.BadRequest
    //    };

    //    yield return new object[]
    //   {
    //        new CreateTemplateBuilder("Template 1").Build(),
    //        "Ya existe una categoría con el mismo nombre",
    //        HttpStatusCode.Forbidden
    //   };
    //}
    //[Theory]
    //[MemberData(nameof(GetTemplateRequests))]
    //public async Task CreateTemplateRequest_ShouldReturnSuccess_IfTemplateRequestCreatedSuccessfully(CreateTemplateRequest createCategory, string errorMessage, HttpStatusCode httpStatusCodeExpected)
    //{

    //    var _httpClient = _factory.CreateClientWithMocks(services =>
    //    {
    //        var apiTemplateRepository = GetCategorySeedRepository();
    //        apiTemplateRepository.AddRangeAsync(SeedTemplateData.SeedTemplate());
    //        apiTemplateRepository.SaveChangesAsync();
    //        services.AddTransient<IRepository<Core.TemplateAggregate.Template>>(provider => apiTemplateRepository);
    //    });
    //    var jsonData = JsonSerializer.Serialize(createCategory);
    //    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
    //    // Act
    //    var response = await _httpClient.PostAsync($"Bsol/BusinessApiCustomer/v1{_url}", content, default);

    //    if (!response.IsSuccessStatusCode)
    //    {
    //        var responseContent = await response.Content.ReadAsStringAsync();
    //        //responseContent.ShouldBe(errorMessage);

    //    }
    //    else
    //    {
    //        response.EnsureSuccessStatusCode();
    //    }
    //    response.StatusCode.ShouldBe(httpStatusCodeExpected);
    //}
}
