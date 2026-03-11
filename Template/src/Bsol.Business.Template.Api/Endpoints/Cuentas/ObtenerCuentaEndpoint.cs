using Bsol.Business.Template.Core.Interfaces;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bsol.Business.Template.Api.Endpoints.Cuentas;

public class ObtenerCuentaEndpoint(IMediator mediator)
    : Endpoint<ObtenerCuentaRequest, Results<Ok<ObtenerCuentaResponse>, NotFound>>
{
    public override void Configure()
    {
        //Version(1);
        Get("/accounts/{Id}");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<ObtenerCuentaResponse>, NotFound>> ExecuteAsync(
        ObtenerCuentaRequest request,
        CancellationToken ct)
    {
        var cuenta = await mediator.Send(new ObtenerCuentaQuery(request.Id), ct);

        if (cuenta == null)
            return TypedResults.NotFound();

        var response = new ObtenerCuentaResponse
        {
            Id = cuenta.Id,
            NumeroCuenta = cuenta.NumeroCuenta,
            Saldo = cuenta.Saldo
        };

        return TypedResults.Ok(response);
    }
}

