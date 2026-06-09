using ConstructFlow.Auth.Domain.Abstractions.Repositories;
using ConstructFlow.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Shared.Infra.Services;

namespace ConstructFlow.Auth.Infrastructure.Persistance.Repositories;

public class RefreshTokenRepository(DbSet<RefreshToken> dbSet)
    : GenericRepository<RefreshToken>(dbSet), IRefreshTokenRepository
{
    private readonly DbSet<RefreshToken> _refreshTokens = dbSet;

    public async Task<RefreshToken?> GetExistingTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        return await _refreshTokens
            .FirstOrDefaultAsync(rt => rt.TokenHash == refreshToken, cancellationToken);
    }
}