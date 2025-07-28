using Microsoft.Extensions.DependencyInjection;
using RuleXEngineProject.Builder;
using RuleXEngineProject.Engine;
using RuleXEngineProject.Logging;
using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRuleEngine<T>(this IServiceCollection services)
        {
            services.AddSingleton<IRuleLogger, ConsoleRuleLogger>();
            services.AddTransient<IRuleEngineBuilder<T>, RuleEngineBuilder<T>>();
            return services;
        }

        public static IServiceCollection AddRuleEngine<T>(this IServiceCollection services, Action<IRuleEngineBuilder<T>> configure)
        {
            services.AddRuleEngine<T>();
            
            var serviceProvider = services.BuildServiceProvider();
            var builder = serviceProvider.GetRequiredService<IRuleEngineBuilder<T>>();
            configure(builder);
            
            var engine = builder.Build();
            services.AddSingleton<IRuleEngine<T>>(engine);
            
            return services;
        }

        public static IServiceCollection AddRule<T, TRule>(this IServiceCollection services) 
            where TRule : class, IRule<T>
        {
            services.AddTransient<IRule<T>, TRule>();
            return services;
        }
    }
}