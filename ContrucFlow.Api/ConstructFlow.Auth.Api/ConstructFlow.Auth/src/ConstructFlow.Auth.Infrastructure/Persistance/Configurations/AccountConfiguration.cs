using ConstructFlow.Auth.Domain.Entities;
using ConstructFlow.Auth.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConstructFlow.Auth.Infrastructure.Persistance.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
                 .ValueGeneratedNever();

        builder.Property(a => a.Email)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.Password)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(a => a.Username)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.ResetPasswordToken)
            .HasMaxLength(200);
        builder.HasIndex(a => a.Username).IsUnique();
        builder.HasIndex(a => a.Email).IsUnique();
        builder.Property(a => a.Role)
            .HasConversion(a => a.Value, value => Role.FromString(value))
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("account_role");
    }
}
