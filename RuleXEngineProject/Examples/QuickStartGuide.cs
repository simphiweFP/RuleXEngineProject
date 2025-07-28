using RuleXEngineProject.Builder;
using RuleXEngineProject.Engine;
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
    /// Quick start guide demonstrating the key features of SmartRules
    /// </summary>
    public static class QuickStartGuide
    {
        /// <summary>
        /// 30-second quick start example
        /// </summary>
        public static void QuickStart()
        {
            Console.WriteLine("🚀 SmartRules - 30 Second Quick Start");
            Console.WriteLine("=" + new string('=', 40));

            // 1. Create your input data
            var applicant = new RuleInputModel
            {
                Age = 25,
                Country = CountryCode.ZA,
                Income = 45000,
                SmokingStatus = SmokingStatus.NonSmoker,
                OccupationRisk = RiskLevel.Low,
                MedicalConditionStatus = MedicalConditionStatus.None,
                NumberOfDependents = 1,
                EmploymentStatus = EmploymentStatus.Employed,
                MaxCover = 150000
            };

            // 2. Build your rule engine fluently
            var engine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .AddRule<SmokerRule>()
                .AddRule<MedicalConditionRule>()
                .AddRule<MaximumCoverRule>()
                .WithExecutionMode(ExecutionMode.AllPass)
                .WithLogger(new ConsoleRuleLogger())
                .EnableTracing()
                .Build();

            // 3. Evaluate with different modes dynamically
            Console.WriteLine("\n📋 Testing All Execution Modes:");
            
            foreach (ExecutionMode mode in Enum.GetValues<ExecutionMode>())
            {
                var result = engine.Evaluate(applicant, mode);
                
                Console.WriteLine($"\n🔍 {mode} Mode:");
                Console.WriteLine($"   ✅ Success: {result.IsSuccessful}");
                Console.WriteLine($"   📊 Score: {result.Score:F1}%");
                Console.WriteLine($"   ⏱️  Time: {result.ExecutionTime.TotalMilliseconds:F1}ms");
                Console.WriteLine($"   📈 Passed: {result.PassedRules}/{result.TotalRules}");
            }

            Console.WriteLine("\n🎉 That's it! You've just used all the key features:");
            Console.WriteLine("   ✓ Dynamic execution modes (AllPass, FirstFail, Scored)");
            Console.WriteLine("   ✓ Fluent rule setup with method chaining");
            Console.WriteLine("   ✓ Rule execution logging and tracing");
            Console.WriteLine("   ✓ DI-compatible and traceable rule engine");
        }

        /// <summary>
        /// Demonstrates the power of execution modes
        /// </summary>
        public static void ExecutionModeShowcase()
        {
            Console.WriteLine("\n🎯 Execution Mode Showcase");
            Console.WriteLine("=" + new string('=', 30));

            // Create an input that will fail some rules
            var problematicApplicant = new RuleInputModel
            {
                Age = 19,                                    // ❌ Too young
                Country = CountryCode.US,                    // ❌ Wrong country
                SmokingStatus = SmokingStatus.Smoker,        // ❌ Smoker
                OccupationRisk = RiskLevel.Low,              // ✅ Good
                MedicalConditionStatus = MedicalConditionStatus.None, // ✅ Good
                NumberOfDependents = 2,                      // ✅ Good
                EmploymentStatus = EmploymentStatus.Employed, // ✅ Good
                Income = 50000,
                MaxCover = 100000
            };

            var engine = new RuleEngineBuilder<RuleInputModel>()
                .AddRule<AgeRule>()
                .AddRule<CountryRule>()
                .AddRule<SmokerRule>()
                .AddRule<OccupationRiskRule>()
                .AddRule<MedicalConditionRule>()
                .AddRule<NumberOfDependentsRule>()
                .AddRule<EmployedStatusRule>()
                .WithLogger(new ConsoleRuleLogger())
                .EnableTracing()
                .Build();

            Console.WriteLine("\n🔍 AllPass Mode - Evaluates ALL rules regardless of failures:");
            var allPassResult = engine.Evaluate(problematicApplicant, ExecutionMode.AllPass);
            Console.WriteLine($"   Result: {allPassResult.PassedRules}/{allPassResult.TotalRules} rules passed");

            Console.WriteLine("\n⚡ FirstFail Mode - Stops at FIRST failure:");
            var firstFailResult = engine.Evaluate(problematicApplicant, ExecutionMode.FirstFail);
            Console.WriteLine($"   Result: Stopped after {firstFailResult.Results.Count} rules");

            Console.WriteLine("\n📊 Scored Mode - Calculates success percentage:");
            var scoredResult = engine.Evaluate(problematicApplicant, ExecutionMode.Scored);
            Console.WriteLine($"   Result: {scoredResult.Score:F1}% success rate");
        }
    }
}