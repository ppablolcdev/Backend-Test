using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.Core.Interfaces;
using Bsol.Business.Template.Core.TemplateAggregate;


namespace Bsol.Business.Template.Core.UseCases.Transacciones;
public class TransferirDineroHandler
{
    private readonly IRepositorioCuenta _repositorioCuenta;
    private readonly IRepositorioTransaccion _repositorioTransaccion;

    public TransferirDineroHandler(
        IRepositorioCuenta repositorioCuenta,
        IRepositorioTransaccion repositorioTransaccion)
    {
        _repositorioCuenta = repositorioCuenta;
        _repositorioTransaccion = repositorioTransaccion;
    }

    public async Task<TransferirResult> Handle(
        TransferirCommand request,
        CancellationToken cancellationToken)
    {
        var cuentaOrigen =
            await _repositorioCuenta.ObtenerPorNumero(request.SourceAccountNumber);

        var cuentaDestino =
            await _repositorioCuenta.ObtenerPorNumero(request.DestinationAccountNumber);

        if (cuentaOrigen == null || cuentaDestino == null)
            throw new Exception("Cuenta no encontrada");

        if (cuentaOrigen.Saldo < request.Amount)
            throw new Exception("Fondos insuficientes");

        cuentaOrigen.Debitar(request.Amount);
        cuentaDestino.Acreditar(request.Amount);

        await _repositorioCuenta.Actualizar(cuentaOrigen);
        await _repositorioCuenta.Actualizar(cuentaDestino);

        var transaccion = new Transaccion
        {
            Id = Guid.NewGuid(),
            CuentaOrigenId = cuentaOrigen.Id,
            CuentaDestinoId = cuentaDestino.Id,
            Monto = request.Amount,
            Fecha = DateTime.UtcNow,
            CodigoVoucher = Guid.NewGuid().ToString("N")[..8]
        };

        await _repositorioTransaccion.Registrar(transaccion);

        return new TransferirResult
        {
            TransactionId = transaccion.Id,
            VoucherCode = transaccion.CodigoVoucher
        };
    }
}
