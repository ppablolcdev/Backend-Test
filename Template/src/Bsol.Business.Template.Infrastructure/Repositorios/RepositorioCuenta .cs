using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.Core.Interfaces;
using Bsol.Business.Template.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bsol.Business.Template.Infrastructure.Repositorios;
public class RepositorioCuenta : IRepositorioCuenta
{
    private readonly AppDbContext _contexto;

    public RepositorioCuenta(AppDbContext contexto)
    {
        _contexto = contexto;
    }
    public async Task Actualizar(Cuenta cuenta)
    {
        _contexto.Cuentas.Update(cuenta);
        await _contexto.SaveChangesAsync();
    }

    public async Task<Cuenta> ObtenerPorId(Guid id)
    {

        return await _contexto.Cuentas
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async  Task<Cuenta> ObtenerPorNumero(string numeroCuenta)
    {
        return await _contexto.Cuentas
           .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
    }
}
