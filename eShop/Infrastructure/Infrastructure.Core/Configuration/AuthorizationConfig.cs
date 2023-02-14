namespace Infrastructure.Core.Configuration;

public class AuthorizationConfig
{
    public string Authority { get; set; } = null!;
    public string SiteAudience { get; set; } = null!;
}