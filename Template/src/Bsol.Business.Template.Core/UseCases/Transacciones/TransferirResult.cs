using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsol.Business.Template.Core.UseCases.Transacciones;
public class TransferirResult
{
    public Guid TransactionId { get; set; }

    public string VoucherCode { get; set; }
}
