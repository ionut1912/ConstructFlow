using ConstructFlow.Auth.Domain.Exceptions;
using Shared.Domain.Common;

namespace ConstructFlow.Auth.Domain.ValueObjects;

public class Role:ValueObject
{
    public static readonly Role Admin = new("Admin");
    public static readonly Role ProjectManager = new("project_manager");
    public static readonly Role SiteSupervisor = new("site_supervisor");
    public static readonly Role Worker = new("Worker");
    public static readonly Role ClientReadonly = new("client_readonly");

    private Role(string value)
    {
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Role FromString(string role)
    {
        return role.Trim().ToLowerInvariant() switch
        {
            "admin" => Admin,
            "projectmanager"=>ProjectManager,
            "sitesupervisor"=>SiteSupervisor,
            "worker"=>Worker,
            "clientreadonly"=>ClientReadonly,
            _=> throw new InvalidRoleException($"Role {role} is invalid")
        };
    }
}
