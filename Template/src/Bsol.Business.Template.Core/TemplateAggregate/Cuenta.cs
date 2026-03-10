using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsol.Business.Template.Core.TemplateAggregate;
public class Cuenta
{
    public Guid Id { get; set; }

    public string NumeroCuenta { get; set; }

    public decimal Saldo { get; set; }

    public DateTime ActualizadoEn { get; set; }

    // Debita dinero de la cuenta
    public void Debitar(decimal monto)
    {
        if (Saldo < monto)
            throw new Exception("Fondos insuficientes");

        Saldo -= monto;
        ActualizadoEn = DateTime.UtcNow;
    }

    // Acredita dinero a la cuenta
    public void Acreditar(decimal monto)
    {
        Saldo += monto;
        ActualizadoEn = DateTime.UtcNow;
    }
}
