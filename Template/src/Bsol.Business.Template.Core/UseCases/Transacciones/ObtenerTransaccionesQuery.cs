using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Entidades;
using MediatR;

namespace Bsol.Business.Template.Core.UseCases.Transacciones;


public record ObtenerTransaccionesQuery()
    : IRequest<List<Transaccion>>;
