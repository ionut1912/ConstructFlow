using ConstructFlow.Auth.Domain.Abstractions.Repositories;
using ConstructFlow.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Infra.Services;

namespace ConstructFlow.Auth.Infrastructure.Persistance.Repositories;


public class AccountRepository(DbSet<Account> dbSet) : GenericRepository<Account>(dbSet), IAccountRepository
{
    private readonly DbSet<Account> _accounts = dbSet;

    public async Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _accounts
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}