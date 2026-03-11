namespace Bsol.Business.Template.Api.Endpoints.Cuentas;

using Bsol.Business.Template.Core.Entidades;
using MediatR;

public record ObtenerCuentaQuery(Guid Id) : IRequest<Cuenta>;
