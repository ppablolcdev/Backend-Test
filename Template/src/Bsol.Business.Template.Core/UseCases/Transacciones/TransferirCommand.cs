using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bsol.Business.Template.Core.UseCases.Transacciones;
using MediatR;


public record TransferirCommand(
    string SourceAccountNumber,
    string DestinationAccountNumber,
    decimal Amount
) : IRequest<TransferirResult>;
