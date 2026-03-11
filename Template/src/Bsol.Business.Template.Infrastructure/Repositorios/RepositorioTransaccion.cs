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
public class RepositorioTransaccion : IRepositorioTransaccion
{
    private readonly AppDbContext _contexto;

    public RepositorioTransaccion(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task Registrar(Transaccion transaccion)
    {
        _contexto.Transacciones.Add(transaccion);

        await _contexto.SaveChangesAsync();
    }

    public async Task<List<Transaccion>> ObtenerTodas()
    {
        return await _contexto.Transacciones.ToListAsync();
    }
}
