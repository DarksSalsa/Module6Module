using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Core.Identity;

public class ScopeRequirement : IAuthorizationRequirement
{
    public ScopeRequirement()
    {
    }
}
