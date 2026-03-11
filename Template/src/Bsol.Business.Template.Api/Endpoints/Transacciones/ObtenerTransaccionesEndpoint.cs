using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.Core.UseCases.Transacciones;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bsol.Business.Template.Api.Endpoints.Transacciones;

public class ObtenerTransacciones(IMediator mediator)
    : EndpointWithoutRequest<Ok<List<Transaccion>>>
{
    public override void Configure()
    {
        //Version(1);
        Get("/transactions");
        AllowAnonymous();
    }

    public override async Task<Ok<List<Transaccion>>> ExecuteAsync(
        CancellationToken ct)
    {
        var transacciones =
            await mediator.Send(new ObtenerTransaccionesQuery(), ct);

        return TypedResults.Ok(transacciones);
    }
}
