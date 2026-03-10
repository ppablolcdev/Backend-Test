using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.TemplateAggregate;

namespace Bsol.Business.Template.Core.Interfaces;
public interface IRepositorioTransaccion
{
    Task Registrar(Transaccion transaccion);

    Task<List<Transaccion>> ObtenerTodas();
}
