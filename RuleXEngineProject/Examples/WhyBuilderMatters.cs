using RuleXEngineProject.Builder;
using RuleXEngineProject.Engine;
using RuleXEngineProject.Logging;
using RuleXEngineProject.Models;
using RuleXEngineProject.Rules;
using System;
using System.Collections.Generic;

namespace RuleXEngineProject.Examples
{
    /// <summary>
    /// Demonstrates why the Builder pattern is essential for the Rule Engine
    /// </summary>
    public static class WhyBuilderMatters
    {
        /// <summary>
        /// Shows the problems WITHOUT using the Builder pattern
        /// </summary>
        public static void WithoutBuilder()
        {
            Console.WriteLine("❌ WITHOUT Builder Pattern - Complex and Error-Prone:");
            Console.WriteLine("=" + new string('=', 60));

            // 1. Manual rule collection creation - verbose and error-prone
            var rules = new List<IRule<RuleInputModel>>();
            rules.Add(new AgeRule());
            rules.Add(new CountryRule());
            rules.Add(new SmokerRule());
            rules.Add(new MedicalConditionRule());
            rules.Add(new OccupationRiskRule());
            rules.Add(new NumberOfDependentsRule());
            rules.Add(new EmployedStatusRule());
            rules.Add(new MaximumCoverRule());

            // 2. Manual configuration - easy to forget parameters
            var logger = new ConsoleRuleLogger();
            var executionMode = ExecutionMode.AllPass;
            var tracingEnabled = true;

            // 3. Constructor with many parameters - hard to remember order
            var engine = new RuleEngine<RuleInputModel>(
                rules,           // What if I pass null?
                executionMode,   // What if I forget this?
                logger,          // What if I pass wrong logger?
                tracingEnabled   // What if I mix up boolean parameters?
            );

            Console.WriteLine("Problems with this approach:");
            Console.WriteLine("• 🔥 Constructor has too many parameters");
            Console.WriteLine("• 🔥 Easy to pass parameters in wrong order");
            Console.WriteLine("• 🔥 No validation of rule collection");
            Console.WriteLine("• 🔥 Hard to remember all configuration options");
            Console.WriteLine("• 🔥 Difficult to extend with new features");
            Console.WriteLine("• 🔥 No fluent, readable API");
        }

        /// <summary>
        /// Shows the benefits WITH the Builder pattern
        /// </summary>
        public static void WithBuilder()
        {
            Console.WriteLine("\n✅ WITH Builder Pattern - Clean and Intuitive:");
            Console.WriteLine("=" + new string('=', 60));

            // Clean, fluent, self-documenting API
            var engine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()                    // ✓ Type-safe rule addition
                .AddRule<CountryRule>()                // ✓ Clear method names
                .AddRule<SmokerRule>()                 // ✓ One rule at a time
                .AddRule<MedicalConditionRule>()       // ✓ Easy to read
                .AddRule<OccupationRiskRule>()         // ✓ Easy to modify
                .AddRule<NumberOfDependentsRule>()     // ✓ Chainable methods
                .AddRule<EmployedStatusRule>()         // ✓ Self-documenting
                .AddRule<MaximumCoverRule>()           // ✓ Consistent pattern
                .WithExecutionMode(ExecutionMode.AllPass)  // ✓ Clear configuration
                .WithLogger(new ConsoleRuleLogger())       // ✓ Optional parameters
                .EnableTracing()                           // ✓ Feature flags
                .Build();                                  // ✓ Final construction

            Console.WriteLine("Benefits of Builder pattern:");
            Console.WriteLine("• ✅ Fluent, readable API");
            Console.WriteLine("• ✅ Method chaining for clean code");
            Console.WriteLine("• ✅ Type safety at compile time");
            Console.WriteLine("• ✅ Self-documenting configuration");
            Console.WriteLine("• ✅ Easy to extend with new features");
            Console.WriteLine("• ✅ Built-in validation");
            Console.WriteLine("• ✅ Immutable final object");
        }

        /// <summary>
        /// Demonstrates extensibility benefits of Builder
        /// </summary>
        public static void ExtensibilityExample()
        {
            Console.WriteLine("\n🚀 Builder Pattern Extensibility:");
            Console.WriteLine("=" + new string('=', 40));

            // Easy to add new configuration methods without breaking existing code
            var engine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .WithExecutionMode(ExecutionMode.Scored)
                .WithLogger(new ConsoleRuleLogger())
                .EnableTracing()
                // Future extensions could be:
                // .WithTimeout(TimeSpan.FromSeconds(30))
                // .WithRetryPolicy(3)
                // .WithCaching()
                // .WithMetrics()
                // .WithCustomValidator(validator)
                .Build();

            Console.WriteLine("Future extensions are easy to add:");
            Console.WriteLine("• .WithTimeout() - for performance limits");
            Console.WriteLine("• .WithRetryPolicy() - for resilience");
            Console.WriteLine("• .WithCaching() - for performance");
            Console.WriteLine("• .WithMetrics() - for monitoring");
            Console.WriteLine("• .WithCustomValidator() - for validation");
        }

        /// <summary>
        /// Shows different configuration scenarios
        /// </summary>
        public static void ConfigurationScenarios()
        {
            Console.WriteLine("\n⚙️ Different Configuration Scenarios:");
            Console.WriteLine("=" + new string('=', 45));

            // Scenario 1: Minimal configuration
            Console.WriteLine("1. Minimal Configuration:");
            var minimalEngine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .Build(); // Uses defaults
            Console.WriteLine("   ✓ Uses default execution mode and logger");

            // Scenario 2: Performance-focused
            Console.WriteLine("\n2. Performance-Focused:");
            var performanceEngine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .AddRule<SmokerRule>()
                .WithExecutionMode(ExecutionMode.FirstFail) // Stop on first failure
                .Build(); // No tracing for speed
            Console.WriteLine("   ✓ FirstFail mode for fast execution");

            // Scenario 3: Debug/Development
            Console.WriteLine("\n3. Debug/Development:");
            var debugEngine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .AddRule<SmokerRule>()
                .AddRule<MedicalConditionRule>()
                .WithExecutionMode(ExecutionMode.AllPass)
                .WithLogger(new DetailedRuleLogger()) // Detailed logging
                .EnableTracing() // Full tracing
                .Build();
            Console.WriteLine("   ✓ Detailed logging and tracing enabled");

            // Scenario 4: Production scoring
            Console.WriteLine("\n4. Production Scoring:");
            var scoringEngine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .AddRule<SmokerRule>()
                .AddRule<MedicalConditionRule>()
                .AddRule<OccupationRiskRule>()
                .AddRule<NumberOfDependentsRule>()
                .AddRule<EmployedStatusRule>()
                .AddRule<MaximumCoverRule>()
                .WithExecutionMode(ExecutionMode.Scored) // Calculate scores
                .WithLogger(new ConsoleRuleLogger())
                .Build();
            Console.WriteLine("   ✓ Scored mode for risk assessment");
        }

        /// <summary>
        /// Demonstrates validation benefits
        /// </summary>
        public static void ValidationBenefits()
        {
            Console.WriteLine("\n🛡️ Built-in Validation Benefits:");
            Console.WriteLine("=" + new string('=', 35));

            try
            {
                // This will throw an exception - no rules added
                var invalidEngine = new RuleEngineBuilder<RuleInputModel>()
                    .WithExecutionMode(ExecutionMode.AllPass)
                    .WithLogger(new ConsoleRuleLogger())
                    .Build(); // This will fail!
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✅ Builder caught error: {ex.Message}");
                Console.WriteLine("   Builder validates configuration before creating engine");
            }

            Console.WriteLine("\nValidation features:");
            Console.WriteLine("• ✅ Ensures at least one rule is added");
            Console.WriteLine("• ✅ Validates logger is not null");
            Console.WriteLine("• ✅ Prevents invalid configurations");
            Console.WriteLine("• ✅ Fails fast with clear error messages");
        }

        /// <summary>
        /// Run all examples to show Builder importance
        /// </summary>
        public static void RunAllExamples()
        {
            Console.WriteLine("🏗️ Why Builder Pattern Matters for SmartRules");
            Console.WriteLine("=" + new string('=', 50));

            WithoutBuilder();
            WithBuilder();
            ExtensibilityExample();
            ConfigurationScenarios();
            ValidationBenefits();

            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("🎯 Key Takeaways:");
            Console.WriteLine("• Builder pattern makes complex object creation simple");
            Console.WriteLine("• Fluent API improves code readability and maintainability");
            Console.WriteLine("• Type safety prevents runtime errors");
            Console.WriteLine("• Easy to extend without breaking existing code");
            Console.WriteLine("• Built-in validation catches errors early");
            Console.WriteLine("• Self-documenting code reduces learning curve");
        }
    }

    /// <summary>
    /// Enhanced detailed logger for examples
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