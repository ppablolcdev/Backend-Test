using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.Core.Interfaces;
using MediatR;

namespace Bsol.Business.Template.Core.UseCases.Transacciones;
public class ObtenerTransaccionesHandler
    : IRequestHandler<ObtenerTransaccionesQuery, List<Transaccion>>
{
    private readonly IRepositorioTransaccion _repositorioTransaccion;

    public ObtenerTransaccionesHandler(
        IRepositorioTransaccion repositorioTransaccion)
    {
        _repositorioTransaccion = repositorioTransaccion;
    }

    public async Task<List<Transaccion>> Handle(
        ObtenerTransaccionesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repositorioTransaccion.ObtenerTodas();
    }
}
