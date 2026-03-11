using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsol.Business.Template.Core.Entidades;
public class Transaccion
{
    public Guid Id { get; set; }

    public Guid CuentaOrigenId { get; set; }

    public Guid CuentaDestinoId { get; set; }

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public string CodigoVoucher { get; set; }
}
