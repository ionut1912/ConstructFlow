using ConstructFlow.Auth.Domain.Entities;
using Shared.Domain.Interfaces;

namespace ConstructFlow.Auth.Domain.Abstractions.Repositories;

public interface IAccountRepository : IGenericRepository<Account>
{
    Task<Account?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
