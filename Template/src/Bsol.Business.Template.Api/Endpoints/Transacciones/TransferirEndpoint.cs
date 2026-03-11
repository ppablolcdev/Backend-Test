using Bsol.Business.Template.Core.UseCases.Transacciones;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Bsol.Business.Template.Api.Endpoints.Transacciones;

public class TransferirEndpoint(IMediator mediator)
    : Endpoint<TransferirRequest, Ok<TransferirResponse>>
{
    public override void Configure()
    {
        //Version(1);
        Post("/transactions");
        AllowAnonymous();
    }

    public override async Task<Ok<TransferirResponse>> ExecuteAsync(
        TransferirRequest request,
        CancellationToken ct)
    {
        var resultado = await mediator.Send(
            new TransferirCommand(
                request.SourceAccountNumber,
                request.DestinationAccountNumber,
                request.Amount),
            ct);

        var response = new TransferirResponse
        {
            TransactionId = resultado.TransactionId,
            VoucherCode = resultado.VoucherCode
        };

        return TypedResults.Ok(response);
    }
}
