using System.Globalization;
using FluentValidation;
using Hotelier.Application.Common.Managers;
using Hotelier.Application.Common.Models;

namespace Hotelier.Api.Configs;

public static class SettingsConfig
{
    public static IServiceCollection AddSettingsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var cultureInfo = new CultureInfo("tr-TR");
        System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
        ValidatorOptions.Global.LanguageManager.Culture = cultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        services.Configure<TokenSetting>(configuration.GetSection("TokenSetting"));
        services.Configure<ImageSetting>(configuration.GetSection("ImageSetting"));
        services.AddTransient<TokenManager>();
        return services;
    }
}