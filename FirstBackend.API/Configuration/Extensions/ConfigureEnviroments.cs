using FirstBackend.Core.Exсeptions;

namespace FirstBackend.API.Configuration.Extensions;

public static class ConfigureEnviroments
{
    public static void ReadSettingsFromEnviroment(this IConfiguration configuration)
    {
        var secretSettings = configuration.GetSection("SecretSettings").GetChildren();
        configuration.ReadSection(secretSettings);

        var databaseSettings = configuration.GetSection("DatabaseSettings").GetChildren();
        configuration.ReadSection(databaseSettings);
    }

    private static void ReadSection(this IConfiguration configuration, IEnumerable<IConfigurationSection> section)
    {
        foreach (var key in section)
        {
            var value = key.Value;
            var env = configuration[value] ?? throw new ConfigurationMissingException("Не прописаны переменные окружения");
            key.Value = env;
        }
    }
}
