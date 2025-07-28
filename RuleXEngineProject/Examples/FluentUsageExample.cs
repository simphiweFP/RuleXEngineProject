using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RuleXEngineProject.Builder;
using RuleXEngineProject.Engine;
using RuleXEngineProject.Extensions;
using RuleXEngineProject.Logging;
using RuleXEngineProject.Models;
using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleXEngineProject.Examples
{
    /// <summary>
    /// Demonstrates various fluent usage patterns for the Rule Engine
    /// </summary>
    public static class FluentUsageExample
    {
        /// <summary>
        /// Example 1: Basic fluent setup with all execution modes
        /// </summary>
        public static void BasicFluentSetup()
        {
            Console.WriteLine("=== Example 1: Basic Fluent Setup ===\n");

            var input = CreateSampleInput();

            // Fluent builder pattern with method chaining
            var engine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .AddRule<SmokerRule>()
                .AddRule<MedicalConditionRule>()
                .WithExecutionMode(ExecutionMode.AllPass)
                .WithLogger(new ConsoleRuleLogger())
                .EnableTracing()
                .Build();

            // Test all execution modes dynamically
            TestAllExecutionModes(engine, input);
        }

        /// <summary>
        /// Example 2: Dependency Injection with fluent configuration
        /// </summary>
        public static void DependencyInjectionExample()
        {
            Console.WriteLine("\n=== Example 2: Dependency Injection Setup ===\n");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register rule engine with fluent configuration
                    services.AddRuleEngine<RuleInputModel>(builder =>
                        builder.AddRule<AgeRule>()
                               .AddRule<CountryRule>()
                               .AddRule<SmokerRule>()
                               .AddRule<NumberOfDependentsRule>()
                               .WithExecutionMode(ExecutionMode.Scored)
                               .EnableTracing());

                    // Register individual rules for DI
                    services.AddRule<RuleInputModel, EmployedStatusRule>();
                    services.AddRule<RuleInputModel, MaximumCoverRule>();
                    services.AddRule<RuleInputModel, MedicalConditionRule>();
                    services.AddRule<RuleInputModel, OccupationRiskRule>();
                })
                .Build();

            // Get engine from DI container
            var engine = host.Services.GetRequiredService<IRuleEngine<RuleInputModel>>();
            var input = CreateSampleInput();

            var result = engine.Evaluate(input);
            DisplayResults(result);

            host.Dispose();
        }

        /// <summary>
        /// Example 3: Advanced fluent configuration with custom logger
        /// </summary>
        public static void AdvancedFluentConfiguration()
        {
            Console.WriteLine("\n=== Example 3: Advanced Fluent Configuration ===\n");

            var customLogger = new DetailedRuleLogger();
            var input = CreateSampleInput();

            // Create multiple rule collections
            var basicRules = new List<IRule<RuleInputModel>>
            {
                new AgeRule(),
                new CountryRule()
            };

            var riskRules = new List<IRule<RuleInputModel>>
            {
                new SmokerRule(),
                new MedicalConditionRule(),
                new OccupationRiskRule()
            };

            // Fluent setup with rule collections
            var engine = new RuleEngineBuilder<RuleInputModel>()
                .AddRules(basicRules)
                .AddRules(riskRules)
                .AddRule(new EmployedStatusRule())
                .AddRule(new MaximumCoverRule())
                .AddRule(new NumberOfDependentsRule())
                .WithExecutionMode(ExecutionMode.FirstFail)
                .WithLogger(customLogger)
                .EnableTracing()
                .Build();

            var result = engine.Evaluate(input);
            DisplayResults(result);
        }

        /// <summary>
        /// Example 4: Dynamic execution mode switching
        /// </summary>
        public static void DynamicExecutionModes()
        {
            Console.WriteLine("\n=== Example 4: Dynamic Execution Mode Switching ===\n");

            var engine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .AddRule<SmokerRule>()
                .AddRule<MedicalConditionRule>()
                .AddRule<OccupationRiskRule>()
                .WithLogger(new ConsoleRuleLogger())
                .EnableTracing()
                .Build();

            var input = CreateSampleInput();

            // Test each execution mode dynamically
            foreach (ExecutionMode mode in Enum.GetValues<ExecutionMode>())
            {
                Console.WriteLine($"\n--- Testing {mode} Mode ---");
                var result = engine.Evaluate(input, mode);
                
                Console.WriteLine($"Mode: {result.ExecutionMode}");
                Console.WriteLine($"Success: {result.IsSuccessful}");
                Console.WriteLine($"Score: {result.Score:F1}%");
                Console.WriteLine($"Execution Time: {result.ExecutionTime.TotalMilliseconds:F1}ms");
                Console.WriteLine($"Summary: {result.Summary}");
            }
        }

        /// <summary>
        /// Example 5: Rule engine with comprehensive logging and tracing
        /// </summary>
        public static void ComprehensiveLoggingExample()
        {
            Console.WriteLine("\n=== Example 5: Comprehensive Logging & Tracing ===\n");

            var detailedLogger = new DetailedRuleLogger();
            var input = CreateFailingInput(); // Use input that will fail some rules

            var engine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .AddRule<SmokerRule>()
                .AddRule<MedicalConditionRule>()
                .AddRule<OccupationRiskRule>()
                .AddRule<NumberOfDependentsRule>()
                .AddRule<EmployedStatusRule>()
                .AddRule<MaximumCoverRule>()
                .WithExecutionMode(ExecutionMode.AllPass)
                .WithLogger(detailedLogger)
                .EnableTracing()
                .Build();

            var result = engine.Evaluate(input);
            DisplayDetailedResults(result);
        }

        /// <summary>
        /// Helper method to test all execution modes
        /// </summary>
        private static void TestAllExecutionModes(IRuleEngine<RuleInputModel> engine, RuleInputModel input)
        {
            foreach (ExecutionMode mode in Enum.GetValues<ExecutionMode>())
            {
                Console.WriteLine($"\n--- {mode} Mode ---");
                var result = engine.Evaluate(input, mode);
                Console.WriteLine($"Success: {result.IsSuccessful}, Score: {result.Score:F1}%");
            }
        }

        /// <summary>
        /// Creates a sample input that should pass most rules
        /// </summary>
        private static RuleInputModel CreateSampleInput()
        {
            return new RuleInputModel
            {
                Age = 30,
                Country = CountryCode.ZA,
                Income = 50000,
                MaxCover = 100000,
                SmokingStatus = SmokingStatus.NonSmoker,
                OccupationRisk = RiskLevel.Low,
                MedicalConditionStatus = MedicalConditionStatus.None,
                NumberOfDependents = 2,
                EmploymentStatus = EmploymentStatus.Employed
            };
        }

        /// <summary>
        /// Creates an input that will fail several rules for testing
        /// </summary>
        private static RuleInputModel CreateFailingInput()
        {
            return new RuleInputModel
            {
                Age = 19, // Will fail age rule
                Country = CountryCode.US, // Will fail country rule
                Income = 25000,
                MaxCover = 200000,
                SmokingStatus = SmokingStatus.Smoker, // Will fail smoker rule
                OccupationRisk = RiskLevel.High, // Will fail occupation risk rule
                MedicalConditionStatus = MedicalConditionStatus.Chronic, // Will fail medical condition rule
                NumberOfDependents = 7, // Will fail dependents rule
                EmploymentStatus = EmploymentStatus.Unemployed // Will fail employment rule
            };
        }

        /// <summary>
        /// Displays basic rule evaluation results
        /// </summary>
        private static void DisplayResults(RuleEngineResult<RuleInputModel> result)
        {
            Console.WriteLine($"Overall Success: {result.IsSuccessful}");
            Console.WriteLine($"Score: {result.Score:F1}%");
            Console.WriteLine($"Execution Time: {result.ExecutionTime.TotalMilliseconds:F1}ms");
            Console.WriteLine($"Rules Passed: {result.PassedRules}/{result.TotalRules}");
        }

        /// <summary>
        /// Displays detailed rule evaluation results
        /// </summary>
        private static void DisplayDetailedResults(RuleEngineResult<RuleInputModel> result)
        {
            Console.WriteLine("\n=== Detailed Results ===");
            Console.WriteLine($"Execution Mode: {result.ExecutionMode}");
            Console.WriteLine($"Overall Success: {result.IsSuccessful}");
            Console.WriteLine($"Score: {result.Score:F1}%");
            Console.WriteLine($"Execution Time: {result.ExecutionTime.TotalMilliseconds:F1}ms");
            Console.WriteLine($"Total Rules: {result.TotalRules}");
            Console.WriteLine($"Passed: {result.PassedRules}");
            Console.WriteLine($"Failed: {result.FailedRules}");
            Console.WriteLine($"Summary: {result.Summary}");

            Console.WriteLine("\nIndividual Rule Results:");
            foreach (var ruleResult in result.Results)
            {
                var status = ruleResult.IsSuccessful ? "✓" : "✗";
                var color = ruleResult.IsSuccessful ? ConsoleColor.Green : ConsoleColor.Red;
                
                Console.ForegroundColor = color;
                Console.WriteLine($"{status} {ruleResult.RuleName}: {ruleResult.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Run all examples
        /// </summary>
        public static void RunAllExamples()
        {
            Console.WriteLine("🧠 SmartRules - Fluent Rule Engine Examples");
            Console.WriteLine("=" + new string('=', 50));

            BasicFluentSetup();
            DependencyInjectionExample();
            AdvancedFluentConfiguration();
            DynamicExecutionModes();
            ComprehensiveLoggingExample();

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("All examples completed successfully!");
        }
    }

    /// <summary>
    /// Enhanced logger with more detailed output
    /// </summary>
    public class DetailedRuleLogger : IRuleLogger
    {
        public void LogEngineStart<T>(T input, ExecutionMode mode)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"🚀 [ENGINE] Starting evaluation with mode: {mode}");
            Console.WriteLine($"📊 [ENGINE] Input type: {typeof(T).Name}");
            Console.ResetColor();
        }

        public void LogEngineComplete<T>(RuleEngineResult<T> result)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"✅ [ENGINE] Completed in {result.ExecutionTime.TotalMilliseconds:F1}ms");
            Console.WriteLine($"📈 [ENGINE] Score: {result.Score:F1}% | Success: {result.IsSuccessful}");
            Console.WriteLine($"📋 [ENGINE] {result.PassedRules}/{result.TotalRules} rules passed");
            Console.ResetColor();
        }

        public void LogRuleExecution(string ruleName, RuleResult result, TimeSpan executionTime)
        {
            var icon = result.IsSuccessful ? "✓" : "✗";
            var color = result.IsSuccessful ? ConsoleColor.Green : ConsoleColor.Yellow;
            
            Console.ForegroundColor = color;
            Console.WriteLine($"{icon} [RULE] {ruleName} - {(result.IsSuccessful ? "PASS" : "FAIL")} ({executionTime.TotalMilliseconds:F1}ms)");
            Console.WriteLine($"   💬 {result.Message}");
            
            if (result.Premium > 0)
            {
                Console.WriteLine($"   💰 Premium Impact: R{result.Premium:F2}");
            }
            
            Console.ResetColor();
        }

        public void LogError(string ruleName, Exception exception)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ [ERROR] Rule '{ruleName}' failed with exception:");
            Console.WriteLine($"   🔥 {exception.Message}");
            Console.ResetColor();
        }
    }
}