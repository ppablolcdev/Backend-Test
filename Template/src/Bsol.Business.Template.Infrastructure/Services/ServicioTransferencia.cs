using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Infrastructure.Data;

namespace Bsol.Business.Template.Infrastructure.Services;
public class ServicioTransferencia
{
    private readonly AppDbContext _contexto;

    public ServicioTransferencia(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task EjecutarAsync(Func<Task> accion)
    {
        using var transaccion = await _contexto.Database.BeginTransactionAsync();

        try
        {
            await accion();

            await _contexto.SaveChangesAsync();

            await transaccion.CommitAsync();
        }
        catch
        {
            await transaccion.RollbackAsync();
            throw;
        }
    }
}
