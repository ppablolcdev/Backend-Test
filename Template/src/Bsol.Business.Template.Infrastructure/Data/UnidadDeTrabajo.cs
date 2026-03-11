using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bsol.Business.Template.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bsol.Business.Template.Infrastructure.Data;
public class UnidadDeTrabajo : IUnidadDeTrabajo
{
    private readonly AppDbContext _context;
    private IDbContextTransaction _transaction;

    public UnidadDeTrabajo(AppDbContext context)
    {
        _context = context;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }
}
