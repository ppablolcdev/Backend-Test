using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Interfaces;
using Bsol.Business.Template.Core.TemplateAggregate;
using Microsoft.EntityFrameworkCore;

namespace Bsol.Business.Template.Core.UseCases.Transacciones;
public class TransferirDineroHandler
{
    private readonly IRepositorioCuenta _repositorioCuenta;
    private readonly IRepositorioTransaccion _repositorioTransaccion;
    private readonly AppDbContext _contexto;

    public TransferirDineroHandler(
        IRepositorioCuenta repositorioCuenta,
        IRepositorioTransaccion repositorioTransaccion,
        AppDbContext contexto)
    {
        _repositorioCuenta = repositorioCuenta;
        _repositorioTransaccion = repositorioTransaccion;
        _contexto = contexto;
    }

    public async Task<object> Ejecutar(
        string numeroCuentaOrigen,
        string numeroCuentaDestino,
        decimal monto)
    {
        // Iniciamos una transacción de base de datos
        using var transaccionBD = await _contexto.Database.BeginTransactionAsync();

        try
        {
            var cuentaOrigen = await _repositorioCuenta.ObtenerPorNumero(numeroCuentaOrigen);
            var cuentaDestino = await _repositorioCuenta.ObtenerPorNumero(numeroCuentaDestino);

            if (cuentaOrigen == null || cuentaDestino == null)
                throw new Exception("Cuenta no encontrada");

            // Aplicamos reglas de negocio
            cuentaOrigen.Debitar(monto);
            cuentaDestino.Acreditar(monto);

            await _repositorioCuenta.Actualizar(cuentaOrigen);
            await _repositorioCuenta.Actualizar(cuentaDestino);

            var nuevaTransaccion = new Transaccion
            {
                Id = Guid.NewGuid(),
                CuentaOrigenId = cuentaOrigen.Id,
                CuentaDestinoId = cuentaDestino.Id,
                Monto = monto,
                Fecha = DateTime.UtcNow,
                CodigoVoucher = Guid.NewGuid().ToString().Substring(0, 8)
            };

            await _repositorioTransaccion.Registrar(nuevaTransaccion);

            await _contexto.SaveChangesAsync();

            await transaccionBD.CommitAsync();

            return new
            {
                transactionId = nuevaTransaccion.Id,
                voucherCode = nuevaTransaccion.CodigoVoucher
            };
        }
        catch
        {
            await transaccionBD.RollbackAsync();
            throw;
        }
    }
}
