using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CCS.WebApp.Extensions
{
    public static class CoreExtensions
    {
        public static TConfig ConfigurePoco<TConfig>(this IServiceCollection services, IConfiguration configuration, bool validate = true, Action<TConfig, IConfiguration> prepareConfig = null) where TConfig : class, new()
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var config = new TConfig();
            configuration.Bind(config);

            prepareConfig?.Invoke(config, configuration);

            if (validate)
            {
                ValidateConfigurationSection(config);
            }

            services.AddSingleton(config);
            return config;
        }

        private static void ValidateConfigurationSection<TConfig>(TConfig config)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(
                config,
                new ValidationContext(config),
                validationResults,
                true))
            {
                throw new ArgumentException($"Error validating configuration for {typeof(TConfig).Name} :: {string.Join(", ", validationResults.Select(vr => vr.ErrorMessage))}");
            }
        }
    }
}
