namespace Bsol.Business.Template.Api.Endpoints.Cuentas;

public class ObtenerCuentaResponse
{
    public Guid Id { get; set; }

    public string NumeroCuenta { get; set; }

    public decimal Saldo { get; set; }
}
