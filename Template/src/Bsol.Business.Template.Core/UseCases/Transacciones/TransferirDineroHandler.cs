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

    public async Task<object> Ejecutar(
        string numeroCuentaOrigen,
        string numeroCuentaDestino,
        decimal monto)
    {
        var cuentaOrigen = await _repositorioCuenta.ObtenerPorNumero(numeroCuentaOrigen);
        var cuentaDestino = await _repositorioCuenta.ObtenerPorNumero(numeroCuentaDestino);

        if (cuentaOrigen == null || cuentaDestino == null)
            throw new Exception("Cuenta no encontrada");

        cuentaOrigen.Debitar(monto);
        cuentaDestino.Acreditar(monto);

        await _repositorioCuenta.Actualizar(cuentaOrigen);
        await _repositorioCuenta.Actualizar(cuentaDestino);

        var transaccion = new Transaccion
        {
            Id = Guid.NewGuid(),
            CuentaOrigenId = cuentaOrigen.Id,
            CuentaDestinoId = cuentaDestino.Id,
            Monto = monto,
            Fecha = DateTime.UtcNow,
            CodigoVoucher = Guid.NewGuid().ToString().Substring(0, 8)
        };

        await _repositorioTransaccion.Registrar(transaccion);

        return new
        {
            transactionId = transaccion.Id,
            voucherCode = transaccion.CodigoVoucher
        };
    }
}
