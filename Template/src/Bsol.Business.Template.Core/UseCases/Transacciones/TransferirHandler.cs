using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.Core.Interfaces;
using Bsol.Business.Template.Core.TemplateAggregate;
using MediatR;


namespace Bsol.Business.Template.Core.UseCases.Transacciones;
public class TransferirHandler
    : IRequestHandler<TransferirCommand, TransferirResult>
{
    private readonly IRepositorioCuenta _repositorioCuenta;
    private readonly IRepositorioTransaccion _repositorioTransaccion;

    private readonly IUnidadDeTrabajo _unidadDeTrabajo;

    public TransferirHandler(
        IRepositorioCuenta repositorioCuenta,
        IRepositorioTransaccion repositorioTransaccion,
        IUnidadDeTrabajo unidadDeTrabajo
         )
    {
        _repositorioCuenta = repositorioCuenta;
        _repositorioTransaccion = repositorioTransaccion;
        _unidadDeTrabajo = unidadDeTrabajo;
    }
    
    public async Task<TransferirResult> Handle(
        TransferirCommand request,
        CancellationToken cancellationToken)
    {
        var cuentaOrigen =
            await _repositorioCuenta.ObtenerPorNumero(request.SourceAccountNumber);

        var cuentaDestino =
            await _repositorioCuenta.ObtenerPorNumero(request.DestinationAccountNumber);

        if (request.Amount <=0)
            throw new Exception("Monto no puede ser menor a 0");

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


    // Nota:
    // En una implementación real se utilizaría un patrón Unit of Work
    // para manejar una transacción de base de datos y garantizar la
    // atomicidad entre el débito y el crédito.
    //
    // Debido a que el proyecto actualmente utiliza una base de datos
    // en memoria no soporta transacciones reales,
    // se deja comentada la implementación del UnitOfWork.
    //

   
    /*
    public async Task<TransferirResult> Handle(
    TransferirCommand request,
    CancellationToken cancellationToken)
    {
        await _unidadDeTrabajo.BeginTransactionAsync();

        try
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

            await _unidadDeTrabajo.CommitAsync();

            return new TransferirResult
            {
                TransactionId = transaccion.Id,
                VoucherCode = transaccion.CodigoVoucher
            };
        }
        catch
        {
            await _unidadDeTrabajo.RollbackAsync();
            throw;
        }
    }*/
}
