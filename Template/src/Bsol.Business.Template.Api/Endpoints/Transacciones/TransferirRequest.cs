namespace Bsol.Business.Template.Api.Endpoints.Transacciones;

public class TransferirRequest
{
    public string SourceAccountNumber { get; set; }

    public string DestinationAccountNumber { get; set; }

    public decimal Amount { get; set; }
}
