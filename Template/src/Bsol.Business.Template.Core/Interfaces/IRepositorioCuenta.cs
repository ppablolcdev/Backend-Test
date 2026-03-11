using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Entidades;

namespace Bsol.Business.Template.Core.Interfaces;
public interface IRepositorioCuenta
{
    Task<Cuenta> ObtenerPorNumero(string numeroCuenta);

    Task<Cuenta> ObtenerPorId(Guid id);

    Task Actualizar(Cuenta cuenta);
}
