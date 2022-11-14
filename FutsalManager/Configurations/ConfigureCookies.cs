namespace FutsalManager.Server.Configurations;

public static class ConfigureCookieSettings
{
    public const int ValidityMinutesPeriod = 60;
    public const string IdentifierCookieName = "EshopIdentifier";

    public static IServiceCollection AddCookieSettings(this IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
        });
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(ValidityMinutesPeriod);
            options.Cookie = new CookieBuilder
            {
                Name = IdentifierCookieName
            };
        });

        return services;
    }
}