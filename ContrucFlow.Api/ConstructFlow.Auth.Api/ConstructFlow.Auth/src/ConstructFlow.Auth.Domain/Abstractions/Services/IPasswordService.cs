
namespace ConstructFlow.Auth.Domain.Abstractions.Services;

public interface IPasswordService
{
    string Hash(string password);
    bool Verify(string password,string hashed);
}
