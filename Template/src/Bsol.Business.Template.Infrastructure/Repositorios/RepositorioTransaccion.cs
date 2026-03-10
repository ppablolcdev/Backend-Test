using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Entidades;
using Bsol.Business.Template.Core.Interfaces;

namespace Bsol.Business.Template.Infrastructure.Repositorios;
public class RepositorioTransaccion : IRepositorioTransaccion
{
    public Task<List<Transaccion>> ObtenerTodas()
    {
        throw new NotImplementedException();
    }

    public Task Registrar(Transaccion transaccion)
    {
        throw new NotImplementedException();
    }
}
