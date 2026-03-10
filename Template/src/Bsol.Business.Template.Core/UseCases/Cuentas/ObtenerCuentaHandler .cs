using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsol.Business.Template.Core.UseCases.Cuentas;

using Bsol.Business.Template.Api.Endpoints.Cuentas;
using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.Core.Interfaces;
using MediatR;

public class ObtenerCuentaHandler
    : IRequestHandler<ObtenerCuentaQuery, Cuenta>
{
    private readonly IRepositorioCuenta _repositorio;

    public ObtenerCuentaHandler(IRepositorioCuenta repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<Cuenta> Handle(
        ObtenerCuentaQuery request,
        CancellationToken cancellationToken)
    {
        return await _repositorio.ObtenerPorId(request.Id);
    }
}
